namespace Core
{
	public sealed class Validation
	{
		public EResult Validate(int[][] field)
		{
			int countZero = 0;
			int sumFirstDiagonalLine = 0;
			int sumSecondDiagonalLine = 0;
            
			for (int i = 0; i < field.Length; i++)
			{
				int sumHorizontalLine = 0;
				int sumVerticalLine = 0;
                
				for (int j = 0; j < field.Length; j++)
				{
					sumHorizontalLine += field[i][j];
					sumVerticalLine += field[j][i];
					if (field[i][j] == 0)
					{
						countZero += 1;
					}
				}
                
				sumFirstDiagonalLine += field[i][i];
				sumSecondDiagonalLine += field[i][(field.Length - 1) - i];

				if (sumHorizontalLine == 3 || sumVerticalLine == 3)
					return EResult.CrossWin;

				if (sumHorizontalLine == -3 || sumVerticalLine == -3)
					return EResult.OWin;
			}

			if (sumFirstDiagonalLine == 3 || sumSecondDiagonalLine == 3)
				return EResult.CrossWin;

			if (sumSecondDiagonalLine == -3 || sumFirstDiagonalLine == -3)
				return EResult.OWin;

			if (countZero == 0)
				return EResult.Draw;
			
			return EResult.NotFinished;
		}

	}
}