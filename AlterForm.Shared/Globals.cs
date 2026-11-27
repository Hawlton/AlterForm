namespace AlterForm.Shared;

public class Globals
{
    public static long max_file_size = 10 * 1024 * 1024;
    public static string alter_theme = Environment.GetEnvironmentVariable("ALTERFORM_THEME") ?? "material-dark";

}
