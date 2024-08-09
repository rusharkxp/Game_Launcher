using System.IO;

namespace Utility
{
    public class ScoreController
    {
        public void SaveScore(int score, string path)
        {
            if (!Directory.Exists(RuntimeConstants.SaveTemplateName))
            {
                Directory.CreateDirectory(RuntimeConstants.SaveTemplateName);
            }

            File.WriteAllText(RuntimeConstants.SaveTemplateName + path, score.ToString());
        }
        
        public int LoadScore(string path)
        {
            if (!File.Exists(RuntimeConstants.SaveTemplateName + path))
            {
                return 0;
            }

            int.TryParse(File.ReadAllText(RuntimeConstants.SaveTemplateName + path), out var loadedScore);

            return loadedScore;
        }
    }
}