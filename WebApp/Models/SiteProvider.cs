namespace WebApp.Models
{
    public class SiteProvider:BaseProvider
    {
        UploadRepository upload;
        public UploadRepository Upload
        {
            get
            {
                if (upload is null)
                {
                    upload = new UploadRepository();
                }
                return upload;
            }
        }

        MemberRepository member;
        public MemberRepository Member
        {
            get
            {
                if(member is null)
                {
                    member = new MemberRepository(Context);
                }
                return member;
            }
        }
        JourneyRepository journey;
        public JourneyRepository Journey
        {
            get
            {
                if (journey is null)
                {
                    journey = new JourneyRepository(Context);
                }
                return journey;
            }
        }

        StatusRepository status;
        public StatusRepository Status
        {
            get
            {
                if (status is null)
                {
                    //status = new StatusRepository(Context);
                    status = new StatusRepository();
                }
                return status;
            }
        }
    }
}
