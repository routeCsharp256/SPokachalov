using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.AggregationModels.MerchPackAggregate
{
    public sealed class MerchPackType : Enumeration
    {
        public static MerchPackType WelcomePack = new(10, "Welcome-pack");
        public static MerchPackType ConferenceListenerPack = new(20, "Conference-listener-pack");
        public static MerchPackType ConferenceSpeakerPack = new(30, "Conference-speaker-pack");
        public static MerchPackType ProbationPeriodEndingPack  = new(40, "Probation-period-ending-pack");
        public static MerchPackType VeteranPack = new(50, "Veteran-pack");

        public MerchPackType(int id, string name) : base(id, name)
        {
        }
    }
}