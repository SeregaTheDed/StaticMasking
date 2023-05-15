using Microsoft.SqlServer.Management.Smo;

namespace StaticMaskingLibrary.MaskingClasses
{
    internal class ForeignKeyUpdater
    {
        public MaskingOptions MaskingOptions { get; }
        private ForeignKeyAction[] lastForeignKeyActions;
        private IEnumerable<ForeignKey> ForeignKeysActionsForChangeUpdateCascade;
        private Random rnd = new Random();
        internal ForeignKeyUpdater(MaskingOptions maskingOptions)
        {
            MaskingOptions = maskingOptions;
        }
        private ForeignKey CloneForeignKey(ForeignKey foreignKey)
        {
            ForeignKey newKey = new ForeignKey(foreignKey.Parent, foreignKey.Name);
            newKey.ReferencedTable = foreignKey.ReferencedTable;
            newKey.ReferencedTableSchema = foreignKey.ReferencedTableSchema;
            newKey.UpdateAction = foreignKey.UpdateAction;
            newKey.DeleteAction = foreignKey.DeleteAction;
            foreach (ForeignKeyColumn column in foreignKey.Columns)
            {
                ForeignKeyColumn fkc;
                fkc = new ForeignKeyColumn(newKey, column.Name, column.ReferencedColumn);
                newKey.Columns.Add(fkc);
            }
            //newKey.Refresh();
            return newKey;
        }

        internal void ChangeForeignKeyActions()
        {
            ForeignKeysActionsForChangeUpdateCascade = MaskingOptions.Tables.Values
                .SelectMany(x => x.Columns.Values)
                .Select(x => x.ForeignKey)
                .Where(x => x != null && x.UpdateAction != ForeignKeyAction.Cascade)
                .ToArray();
            lastForeignKeyActions = ForeignKeysActionsForChangeUpdateCascade.Select(x => x.UpdateAction).ToArray();

            List<ForeignKey> newForeignKeys = new List<ForeignKey>();
            foreach (var foreignKey in ForeignKeysActionsForChangeUpdateCascade)
            {
                ForeignKey newKey = CloneForeignKey(foreignKey);
                foreignKey.Drop();
                newKey.UpdateAction = ForeignKeyAction.Cascade;
                newKey.Create();
                newForeignKeys.Add(newKey);
            }
            ForeignKeysActionsForChangeUpdateCascade = newForeignKeys;
        }

        internal void ResetForeignKeyActions()
        {
            int i = 0;
            foreach (var foreignKey in ForeignKeysActionsForChangeUpdateCascade)
            {
                ForeignKey newKey = CloneForeignKey(foreignKey);
                foreignKey.Drop();
                newKey.UpdateAction = lastForeignKeyActions[i];
                newKey.Create();
                i++;
            }
        }
    }
}
