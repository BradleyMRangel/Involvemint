namespace WebApplication1.ViewModel.EventViewModelHelpers
{
    public enum Command
    {
        //generic database commands
        GetById,
        GetList,
        Save,
        Clear,
        Insert,
        Delete,
        Update,

        // Domain model class product specific commands
        SortByName,
        InitializeEventDb,
        Autocomplete,
        SortByType,
        ShowCurrentEvents,
        MaxAttendance
    };
}