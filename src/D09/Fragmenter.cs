namespace aoc.D09
{
  public class Fragmenter
  {
    private List<Block> _decodedBlocks;
    private int _freeBlocksCount = 0;
    private int _highestId = 0;

    public Fragmenter(string path, bool isPartII = false)
    {
      var text = File.ReadAllText(path).Trim();
      _decodedBlocks = new List<Block>();
      _highestId = -1;      

      for (int i = 0; i < text.Length; i++)
      {
        if (i % 2 == 0)
        {
          _highestId++;
          var fileBlockCount = int.Parse(text[i].ToString());

          for (int j = 0; j < fileBlockCount; j++)
            _decodedBlocks.Add(new Block(false, _highestId));
        }
        else //odd 
        {
          var freeBlockCount = int.Parse(text[i].ToString());

          for (int j = 0; j < freeBlockCount; j++)
          {
            _decodedBlocks.Add(new Block(true));
            _freeBlocksCount++;
          }
        }
      }

      if (isPartII)
        FragmentII();
      else
        Fragment();
    }

    private void Fragment()
    {
      int start = 0;
      int end = _decodedBlocks.Count - 1;

      for (int j = _freeBlocksCount - 1; j > -1; j--)
      {
        var indexToFill = _decodedBlocks.FindIndex(start, b => b.IsFree == true);
        var indexToMove = _decodedBlocks.FindLastIndex(end, b => b.IsFree == false);

        start = indexToFill;
        end = indexToMove;

        if (indexToFill > indexToMove)
          break;

        var temp = _decodedBlocks[indexToMove].FileId;
        _decodedBlocks[indexToMove].FileId = -1;
        _decodedBlocks[indexToMove].IsFree = true;
        _decodedBlocks[indexToFill].FileId = temp;
        _decodedBlocks[indexToFill].IsFree = false;
      }
    }

    private void FragmentII()
    {
      for(int j = _highestId; j > -1; j--)    //one file at a time i.e all blocks wih same fileId
      {
        int end = _decodedBlocks.FindLastIndex(b => b.FileId == j);
        int start = _decodedBlocks.FindIndex(b => b.FileId == j);
        var blocksNeeded = end - start + 1;
        var indexToFill = GetIndexOfFirstAvailableSpace(blocksNeeded, start);

        if (indexToFill != -1)
        {
          var temp = _decodedBlocks[start].FileId;

          for (int i = start; i < start + blocksNeeded; i++)
          {
            _decodedBlocks[i].FileId = -1;
            _decodedBlocks[i].IsFree = true;
          }

          for (int i = indexToFill; i < indexToFill + blocksNeeded; i++)
          {
            _decodedBlocks[i].FileId = temp;
            _decodedBlocks[i].IsFree = false;
          }
        }
      }
    }

    public long GetChecksum()
    {
      long checksum = 0;

      for (int i = 0; i < _decodedBlocks.Count; i++)
      {
        if (!_decodedBlocks[i].IsFree)
        {
          long product = _decodedBlocks[i].FileId * i;
          checksum += product;
        }
      }

      return checksum;
    }

    //return -1 if no space found, otherwise return index of first available spot
    private int GetIndexOfFirstAvailableSpace(int n, int upperBound)
    {
      if (n <= 0 || n > upperBound)
        return -1;
      
      int currentStartIndex = -1; // start index of the current free block 
      int freeIndicesCount = 0;   // Count of consecutive free indices

      for (int i = 0; i < upperBound; i++)
      {
        if (_decodedBlocks[i].IsFree)
        {
          if (currentStartIndex == -1)    //set start index if not set already           
            currentStartIndex = i;
          
          freeIndicesCount++;
          
          if (freeIndicesCount == n)          
            return currentStartIndex;          
        }
        else // Reset trackers as we hit a file instead
        {
          currentStartIndex = -1;
          freeIndicesCount = 0;
        }
      }

      return -1; //no appropriate place found
    }
  }
}
