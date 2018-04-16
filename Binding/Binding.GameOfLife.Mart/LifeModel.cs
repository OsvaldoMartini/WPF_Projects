using Binding.GameOfLife.Mart.Utility;

namespace Binding.GameOfLife.Mart
{
	
	/// <summary>
	/// LifeModel is the back end model for Conway's Game of Life. The game consist of a two dimensional grid
	/// that has a population of cellular automoton which are in one of two states: either alive or dead.
	/// The rules are simple (see http://en.wikipedia.org/wiki/Conway%27s_Game_of_Life):
	///		1.Any live cell with fewer than two live neighbours dies, as if caused by under-population.
	///		2.Any live cell with more than three live neighbours dies, as if by overcrowding.
	///		3.Any live cell with two or three live neighbours lives on to the next generation.
	///		4.Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
	/// </summary>
	class LifeModel
	{
		LifeTorus _current;
		Dimensions _dim;
		
		public LifeModel(Dimensions dim)
		{
			_dim = dim;
			_current = new LifeTorus(dim);
		}

		public Dimensions Dimension
		{
			get { return _dim; }
			set
			{
				if (value == _dim)
					return;
				LifeTorus lt = new LifeTorus(value);
				_current.CopyTo(lt);
				_current = lt;
				_dim = value;
			}
		}

		public void Clear()
		{
			_current.Clear();
		}

		public bool this[int x, int y]
		{
			get
			{
				return _current[x, y];
			}
			set
			{
				_current[x, y] = value;
			}
		}

		/// <summary>
		/// Advances the game to the next generation.
		/// Returns true if changes have occurred, i.e. any cell was born or has died.
		/// This is typically used to halt any automation once no further changes occur.
		/// In actual fact the game will often enter into sequence where ti alternates between two states, e.g.
		/// the 'blinker'. At this stage we don't detect such repeating sequences.
		/// </summary>
		/// <returns></returns>
		public bool Next()
		{		
			LifeTorus next = new LifeTorus(_dim);
			bool changed = false;
			for (int x = 0; x < _dim.Width; x++)
			{
				for (int y = 0; y < _dim.Height; y++)
				{
					// Calculate the number of alive neighbours to this cell.
					// The grid is a torus with left edge adjacent to the right and top edge adjacent to the
					// bottom edge. This wrap around torus function is implemented in the LifeTorus class so
					// no special coding is required here.
					int neighbours = 0;

					// Top row
					if (_current[x - 1, y - 1])
						neighbours++;
					if (_current[x, y - 1])
						neighbours++;
					if (_current[x + 1, y - 1])
						neighbours++;

					// Bottom row
					if (_current[x - 1, y + 1])
						neighbours++;
					if (_current[x, y + 1])
						neighbours++;
					if (_current[x + 1, y + 1])
						neighbours++;

					// Either side
					if (_current[x - 1, y])
						neighbours++;
					if (_current[x + 1, y])
						neighbours++;

					if (_current[x, y])
					{
						// Its alive now
						if (neighbours == 2 || neighbours == 3)
							next[x, y] = true;
						else
							changed = true;
					}
					else
					{
						// Its dead but will be born if ==3
						if (neighbours == 3)
						{
							next[x, y] = true;
							changed = true;
						}
					}
				}
			}
			_current = next;
			return changed;
		}
	}

}
