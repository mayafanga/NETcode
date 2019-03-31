 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAR.TeamManager.DAL;
using BAR.TeamManager.IDAL;
using BAR.TeamManager.Model;
using System.Data.Entity;

namespace BAR.TeamManager.DALFactory
{
	public partial class DBSession : IDBSession
    {
	
		private IactivitieapplicationDal _activitieapplicationDal;
        public IactivitieapplicationDal activitieapplicationDal
        {
            get
            {
                if(_activitieapplicationDal == null)
                {
                    _activitieapplicationDal = AbstractFactory.createactivitieapplicationDal();
                }
                return _activitieapplicationDal;
            }
            set { _activitieapplicationDal = value; }
        }
	
		private IactivityDal _activityDal;
        public IactivityDal activityDal
        {
            get
            {
                if(_activityDal == null)
                {
                    _activityDal = AbstractFactory.createactivityDal();
                }
                return _activityDal;
            }
            set { _activityDal = value; }
        }
	
		private IadministratorDal _administratorDal;
        public IadministratorDal administratorDal
        {
            get
            {
                if(_administratorDal == null)
                {
                    _administratorDal = AbstractFactory.createadministratorDal();
                }
                return _administratorDal;
            }
            set { _administratorDal = value; }
        }
	
		private IauditDal _auditDal;
        public IauditDal auditDal
        {
            get
            {
                if(_auditDal == null)
                {
                    _auditDal = AbstractFactory.createauditDal();
                }
                return _auditDal;
            }
            set { _auditDal = value; }
        }
	
		private IauthorityDal _authorityDal;
        public IauthorityDal authorityDal
        {
            get
            {
                if(_authorityDal == null)
                {
                    _authorityDal = AbstractFactory.createauthorityDal();
                }
                return _authorityDal;
            }
            set { _authorityDal = value; }
        }
	
		private IcommentDal _commentDal;
        public IcommentDal commentDal
        {
            get
            {
                if(_commentDal == null)
                {
                    _commentDal = AbstractFactory.createcommentDal();
                }
                return _commentDal;
            }
            set { _commentDal = value; }
        }
	
		private IeventapplicantDal _eventapplicantDal;
        public IeventapplicantDal eventapplicantDal
        {
            get
            {
                if(_eventapplicantDal == null)
                {
                    _eventapplicantDal = AbstractFactory.createeventapplicantDal();
                }
                return _eventapplicantDal;
            }
            set { _eventapplicantDal = value; }
        }
	
		private IhonorDal _honorDal;
        public IhonorDal honorDal
        {
            get
            {
                if(_honorDal == null)
                {
                    _honorDal = AbstractFactory.createhonorDal();
                }
                return _honorDal;
            }
            set { _honorDal = value; }
        }
	
		private IhonorparticipantmemberDal _honorparticipantmemberDal;
        public IhonorparticipantmemberDal honorparticipantmemberDal
        {
            get
            {
                if(_honorparticipantmemberDal == null)
                {
                    _honorparticipantmemberDal = AbstractFactory.createhonorparticipantmemberDal();
                }
                return _honorparticipantmemberDal;
            }
            set { _honorparticipantmemberDal = value; }
        }
	
		private IparticipatmembersDal _participatmembersDal;
        public IparticipatmembersDal participatmembersDal
        {
            get
            {
                if(_participatmembersDal == null)
                {
                    _participatmembersDal = AbstractFactory.createparticipatmembersDal();
                }
                return _participatmembersDal;
            }
            set { _participatmembersDal = value; }
        }
	
		private IpersonalinformationDal _personalinformationDal;
        public IpersonalinformationDal personalinformationDal
        {
            get
            {
                if(_personalinformationDal == null)
                {
                    _personalinformationDal = AbstractFactory.createpersonalinformationDal();
                }
                return _personalinformationDal;
            }
            set { _personalinformationDal = value; }
        }
	
		private IplayersDal _playersDal;
        public IplayersDal playersDal
        {
            get
            {
                if(_playersDal == null)
                {
                    _playersDal = AbstractFactory.createplayersDal();
                }
                return _playersDal;
            }
            set { _playersDal = value; }
        }
	
		private IregisterloginDal _registerloginDal;
        public IregisterloginDal registerloginDal
        {
            get
            {
                if(_registerloginDal == null)
                {
                    _registerloginDal = AbstractFactory.createregisterloginDal();
                }
                return _registerloginDal;
            }
            set { _registerloginDal = value; }
        }
	
		private IscoreDal _scoreDal;
        public IscoreDal scoreDal
        {
            get
            {
                if(_scoreDal == null)
                {
                    _scoreDal = AbstractFactory.createscoreDal();
                }
                return _scoreDal;
            }
            set { _scoreDal = value; }
        }
	
		private IteacherDal _teacherDal;
        public IteacherDal teacherDal
        {
            get
            {
                if(_teacherDal == null)
                {
                    _teacherDal = AbstractFactory.createteacherDal();
                }
                return _teacherDal;
            }
            set { _teacherDal = value; }
        }
	
		private IteamDal _teamDal;
        public IteamDal teamDal
        {
            get
            {
                if(_teamDal == null)
                {
                    _teamDal = AbstractFactory.createteamDal();
                }
                return _teamDal;
            }
            set { _teamDal = value; }
        }
	
		private IteamapplicantDal _teamapplicantDal;
        public IteamapplicantDal teamapplicantDal
        {
            get
            {
                if(_teamapplicantDal == null)
                {
                    _teamapplicantDal = AbstractFactory.createteamapplicantDal();
                }
                return _teamapplicantDal;
            }
            set { _teamapplicantDal = value; }
        }
	
		private IteamapplicationDal _teamapplicationDal;
        public IteamapplicationDal teamapplicationDal
        {
            get
            {
                if(_teamapplicationDal == null)
                {
                    _teamapplicationDal = AbstractFactory.createteamapplicationDal();
                }
                return _teamapplicationDal;
            }
            set { _teamapplicationDal = value; }
        }
	
		private IuserDal _userDal;
        public IuserDal userDal
        {
            get
            {
                if(_userDal == null)
                {
                    _userDal = AbstractFactory.createuserDal();
                }
                return _userDal;
            }
            set { _userDal = value; }
        }
	
		private IuserauthorityDal _userauthorityDal;
        public IuserauthorityDal userauthorityDal
        {
            get
            {
                if(_userauthorityDal == null)
                {
                    _userauthorityDal = AbstractFactory.createuserauthorityDal();
                }
                return _userauthorityDal;
            }
            set { _userauthorityDal = value; }
        }
	
		private IwebmasterDal _webmasterDal;
        public IwebmasterDal webmasterDal
        {
            get
            {
                if(_webmasterDal == null)
                {
                    _webmasterDal = AbstractFactory.createwebmasterDal();
                }
                return _webmasterDal;
            }
            set { _webmasterDal = value; }
        }
	}	
}