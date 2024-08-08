using System.IO;

namespace Utility
{
    public class ScoreController
    {
        public void SaveScore(int score, string path)
        {
            File.WriteAllText(path, score.ToString());
        }
        
        public int LoadScore(string path)
        {
            if (!File.Exists(path))
            {
                return 0;
            }

            int.TryParse(File.ReadAllText(path), out var loadedScore);

            return loadedScore;
        }
    }
}