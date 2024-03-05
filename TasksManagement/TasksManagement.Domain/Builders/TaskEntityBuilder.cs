using TasksManagement.Domain.Enums;

namespace TasksManagement.Domain.Entities;

public partial class TaskEntity
{
    internal class TaskEntityBuilder
    {
        private TaskEntity _task = default!;

        public TaskEntityBuilder()
        {
            Reset();
        }

        private void Reset()
        {
            _task = new()
            {
                Status = TaskStatusEnum.Pending
            };
        }

        public TaskEntityBuilder WithCode(long code)
        {
            _task.Code = code;
            return this;
        }

        public TaskEntityBuilder WithName(string name)
        {
            _task.Name = name;
            return this;
        }

        public TaskEntityBuilder WithDescription(string description)
        {
            _task.Description = description;
            return this;
        }

        public TaskEntityBuilder WithPriority(TaskPriorityEnum priority)
        {
            _task.Priority = priority;
            return this;
        }

        public TaskEntity Build()
        {
            var task = _task;
            Reset();
            return task;
        }
    }

}
