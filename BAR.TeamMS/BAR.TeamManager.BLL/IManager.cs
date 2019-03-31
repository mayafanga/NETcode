 
using BAR.TeamManager.IBLL;
using BAR.TeamManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAR.TeamManager.BLL
{
	
	public partial class activitieapplicationService :BaseService<activitieapplication>,IactivitieapplicationService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.activitieapplicationDal;
        }
    }   
	
	public partial class activityService :BaseService<activity>,IactivityService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.activityDal;
        }
    }   
	
	public partial class administratorService :BaseService<administrator>,IadministratorService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.administratorDal;
        }
    }   
	
	public partial class auditService :BaseService<audit>,IauditService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.auditDal;
        }
    }   
	
	public partial class authorityService :BaseService<authority>,IauthorityService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.authorityDal;
        }
    }   
	
	public partial class commentService :BaseService<comment>,IcommentService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.commentDal;
        }
    }   
	
	public partial class eventapplicantService :BaseService<eventapplicant>,IeventapplicantService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.eventapplicantDal;
        }
    }   
	
	public partial class honorService :BaseService<honor>,IhonorService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.honorDal;
        }
    }   
	
	public partial class honorparticipantmemberService :BaseService<honorparticipantmember>,IhonorparticipantmemberService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.honorparticipantmemberDal;
        }
    }   
	
	public partial class participatmembersService :BaseService<participatmembers>,IparticipatmembersService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.participatmembersDal;
        }
    }   
	
	public partial class personalinformationService :BaseService<personalinformation>,IpersonalinformationService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.personalinformationDal;
        }
    }   
	
	public partial class playersService :BaseService<players>,IplayersService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.playersDal;
        }
    }   
	
	public partial class registerloginService :BaseService<registerlogin>,IregisterloginService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.registerloginDal;
        }
    }   
	
	public partial class scoreService :BaseService<score>,IscoreService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.scoreDal;
        }
    }   
	
	public partial class teacherService :BaseService<teacher>,IteacherService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.teacherDal;
        }
    }   
	
	public partial class teamService :BaseService<team>,IteamService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.teamDal;
        }
    }   
	
	public partial class teamapplicantService :BaseService<teamapplicant>,IteamapplicantService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.teamapplicantDal;
        }
    }   
	
	public partial class teamapplicationService :BaseService<teamapplication>,IteamapplicationService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.teamapplicationDal;
        }
    }   
	
	public partial class userService :BaseService<user>,IuserService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.userDal;
        }
    }   
	
	public partial class userauthorityService :BaseService<userauthority>,IuserauthorityService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.userauthorityDal;
        }
    }   
	
	public partial class webmasterService :BaseService<webmaster>,IwebmasterService
    {
    

		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.webmasterDal;
        }
    }   
	
}