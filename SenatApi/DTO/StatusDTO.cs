using Newtonsoft.Json;


namespace SenatApi
{

    public class StatusDto
    {
        [JsonProperty("copyVotes")]
        public bool CopyVotes { get; set; }
        [JsonProperty("isVotingWithAdjustment")]
        public bool IsVotingWithAdjustment { get; set; }
        public StatusDto(bool copyVotes, bool isVotingWuthAdjustment)
        { CopyVotes = copyVotes; IsVotingWithAdjustment = isVotingWuthAdjustment; }
    }
}
