using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amalgamator
{
    public class Amalgamator
    {
        private List<string> partials;
        public Amalgamator(List<string> partials) {
            this.partials = partials;
        }
        public string amalgamate()
        {
            //to save the original
            var segments = new List<string>(partials);
            while(segments.Count > 1)
            {
                var candidates = generateCandidates(segments);
                candidates.Sort((a, b) => b.overlapCount.CompareTo(a.overlapCount));
                var bestCandidate = candidates[0];
                //replace first string with merged string
                segments[segments.FindIndex(segment => segment == bestCandidate.lhs)] = bestCandidate.mergedString();
                //and just remove the second string
                segments.Remove(bestCandidate.rhs);
            }
            return segments.First();
        }
        private List<Overlap> generateCandidates(List<string> segments)
        {
            var candidates = new List<Overlap>();
            for (var i = 0; i < segments.Count; i++)
            {
                for (var k = i + 1; k < segments.Count; k++)
                {
                    //put both ways into candidates
                    candidates.Add(new Overlap(segments[i], segments[k]));
                    candidates.Add(new Overlap(segments[k], segments[i]));
                }
            }
            return candidates;
        }
    }
    public class Overlap
    {
        public int overlapCount { get; set; } = 0;
        public int overlapStartIndex { get; set;  } = 0;
        public string lhs { get; set; }
        public string rhs { get; set; }

        public Overlap(string a, string b)
        {
            lhs = a;
            rhs = b;
            calculateOverlap();
        }
        private void calculateOverlap() {
            for (int startIndex = lhs.IndexOf(rhs[0]); startIndex > -1; startIndex = lhs.IndexOf(rhs[0], startIndex + 1))
            {
                overlapCount = 0;
                overlapStartIndex = startIndex;
                int i = startIndex;
                
                foreach (char c in rhs)
                {
                    //if we ran past the end of a, we are done and this is a good candidate to merge
                    //because we ran out the end we know that any other matches of starting index will have less overlapping characters since there is less string to work with
                    if (i >= lhs.Length)
                    {
                        return; //we found what we want so no need to go on
                    }
                    //otherwise if the chars are equal, we are still a possible candidate
                    if (lhs[i] == c)
                    {
                        overlapCount++;
                        i += 1;
                    }
                    else
                    {
                        //this starting index didnt work but there might be another
                        //there is no overlap and the merge should start at the end of lhs
                        overlapCount = 0;
                        overlapStartIndex = lhs.Length;
                        break;
                    }
                }
            }
        }
        public string mergedString() {
            if (overlapStartIndex + overlapCount < lhs.Length)
            {
                return lhs;
            }
            return String.Concat(lhs, rhs.Substring(overlapCount));
        }
    }
}
