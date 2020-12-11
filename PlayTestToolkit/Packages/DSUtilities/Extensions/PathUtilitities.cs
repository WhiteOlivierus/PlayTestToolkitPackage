namespace Dutchskull.Utilities.Extensions
{
    public static class PathUtilitities
    {
        public static string PathBuilder(string root, string[] folders, string fileName) =>
            $"{root}{string.Join("/", folders)}/{fileName}";
    }
}
