using TMPro;

namespace UI
{
	public class GameOverPanel : Panel
	{
		public TextMeshProUGUI PlayCount;
		public TextMeshProUGUI BestScore;
		public TextMeshProUGUI WorstScore;

		public void Refresh()
		{
			PlayCount.text = "1";
			BestScore.text = "00:00";
			WorstScore.text = "00:00";
		}
	}
}