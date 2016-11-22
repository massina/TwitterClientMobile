using System.Collections.Generic;

namespace TwitterClientMobile.JsonFormatterTypes
{
    public class Prediction
    {
        public string Description { get; set; }
        public string Id { get; set; }
        public List<MatchedSubstring> Matched_Substrings { get; set; }
        public string Place_Id { get; set; }
        public string Reference { get; set; }
        public List<Term> Terms { get; set; }
        public List<string> Types { get; set; }
    }

    public class GoogleMapPlace
    {
        public List<Prediction> Predictions { get; set; }
        public string Status { get; set; }
    }
    public class MatchedSubstring
    {
        public int Length { get; set; }
        public int Offset { get; set; }
    }

    public class Term
    {
        public int Offset { get; set; }
        public string Value { get; set; }
    }
}
