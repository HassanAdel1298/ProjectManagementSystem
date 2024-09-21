using ProjectManagementSystem.Entity.Entities;


namespace ProjectManagementSystem.Application.ViewModel.Tasks
{
    public class TaskCreateViewModel
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime AssignedDate { get; set; }
        public int ProjectID { get; set; }
        public int UserAssignID { get; set; }

    }
}
