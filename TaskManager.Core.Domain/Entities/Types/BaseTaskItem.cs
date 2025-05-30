namespace TaskManager.Core.Domain.Entities.Types
{
    public abstract class BaseTaskItem : TaskItem
    {
        public abstract string GetTaskType();
    }

    public class BugFixTask : BaseTaskItem
    {
        public override string GetTaskType() => "Bug Fix";
    }

    public class FeatureTask : BaseTaskItem
    {
        public override string GetTaskType() => "Feature";
    }

    public class RefactorTask : BaseTaskItem
    {
        public override string GetTaskType() => "Refactor";
    }

}
