using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Zero_Hunger_Industry_Standard.Dtos;
using Zero_Hunger_Industry_Standard.Models;

namespace Zero_Hunger_Industry_Standard.Controllers
{
    public class RestrurantController : Controller
    {
        // GET: Restrurant
       
        private readonly HungerEntities _context;

        public RestrurantController()
        {
            _context = new HungerEntities(); // Initialize your DbContext here
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async  Task<ActionResult> GetRestrurantDetails(long? RestrurantId) 
        {

            RestrurantDto data = await (from hed in _context.TblRestrurantHeaders                           
                              where hed.IsActive == true
                              select new RestrurantDto
                              {
                                  intRestrurantId = hed.intRestrurantId,
                                  strRestrurantName = hed.strRestrurantName,
                                  strRestrurantType = hed.strRestrurantType,
                                  restrurantFoods = (from qr in _context.TblRestrurantRows
                                                     where qr.intRestrurantId == hed.intRestrurantId && qr.IsActive == true
                                                     select new RestrurantFoodDto
                                                     {
                                                         intRowId = qr.intRowId,
                                                         intRestrurantId = qr.intRestrurantId,
                                                         strFood = qr.strFood,
                                                         StrFoodType = qr.StrFoodType,
                                                         IsCollectReqest = qr.IsCollectReqest,
                                                         DteCollectionTimeLeft = qr.DteCollectionTimeLeft,
                                                         intEmployeeId = qr.intEmployeeId,
                                                     }).ToList(),
                              }).FirstOrDefaultAsync();

            return View(data);        
        }

        [HttpPost]
        public async Task<ActionResult> CreateUpdateDeleteRestrurant(RestrurantDto restrurant)
        {

            ResponseMessgeDto sub = new ResponseMessgeDto();
            try
            {
                var data = await _context.TblRestrurantHeaders.Where(x => x.intRestrurantId == restrurant.intRestrurantId && x.strRestrurantName.ToLower() == restrurant.strRestrurantName.ToLower() && x.IsActive == true).FirstOrDefaultAsync();
                if (data == null)
                {
                    TblRestrurantHeader Res = new TblRestrurantHeader()
                    {
                        strRestrurantName = restrurant.strRestrurantName,
                        strRestrurantType = restrurant.strRestrurantType
                    };
                    _context.TblRestrurantHeaders.Add(Res);
                    await _context.SaveChangesAsync();
                    sub.ResponseCode = 200;
                    sub.Message = "Created Sucessfully";

                }
                else
                {
                    data.strRestrurantName = restrurant.strRestrurantName;
                    data.strRestrurantType = restrurant.strRestrurantType;
                    data.IsActive = restrurant.IsActive;

                    _context.TblRestrurantHeaders.AddOrUpdate(data);
                    await _context.SaveChangesAsync();
                    sub.ResponseCode = 200;
                    sub.Message = "Updated Sucessfully";

                }

                return View(sub);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }




        [HttpPost]
        public async Task<ActionResult> OpenCollectionRequestForm(RestrurantDto restrurant)
        {
            //log For Collection Requests 
            ResponseMessgeDto sub = new ResponseMessgeDto();
            try
            {
                var data = await _context.TblRestrurantHeaders.Where(x => x.intRestrurantId == restrurant.intRestrurantId && x.strRestrurantName.ToLower() == restrurant.strRestrurantName.ToLower() && x.IsActive == true).FirstOrDefaultAsync();

                if (data != null)
                {

                    List<tblLog> lg = new List<tblLog>();

                    foreach (var item in restrurant.restrurantFoods)
                    {
                        TblRestrurantRow row = new TblRestrurantRow()
                        {
                            strFood = item.strFood,
                            StrFoodType = item.StrFoodType,
                            intRestrurantId = item.intRestrurantId,
                            intEmployeeId = item.intEmployeeId,
                            IsCollectReqest = false,
                            DteCollectionTimeLeft = item.DteCollectionTimeLeft,
                            IsActive = true
                        };
                        _context.TblRestrurantRows.Add(row);
                        await _context.SaveChangesAsync();


                        tblLog log = new tblLog()
                        {
                            intRestrurantId = item.intRestrurantId,
                            intRowid = row.intRowId,

                        };
                        lg.Add(log);

                    }
                    _context.tblLogs.AddRange(lg);
                    await _context.SaveChangesAsync();

                    sub.ResponseCode = 200;
                    sub.Message = "Open For Collection";
                }
                else
                {
                    sub.ResponseCode = 400;
                    sub.Message = "Restrurant Not Found ";
                }
                return View(sub);
            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }
        [HttpGet]

        public async Task<ActionResult> GetCollectionRequest(DateTime ? Date)
        {

            var data = await (from s in _context.TblRestrurantRows
                              join sd in _context.TblRestrurantHeaders on s.intRestrurantId equals sd.intRestrurantId
                              where s.DteCollectionTimeLeft >= DateTime.Now || Date == null && s.IsCollectReqest == false
                              select new RestrurantFoodDto
                              {
                                  strFood = s.strFood,
                                  StrFoodType = s.StrFoodType,
                                  intRestrurantId = s.intRestrurantId,
                                  intEmployeeId = s.intEmployeeId,
                                  IsCollectReqest = s.IsCollectReqest,
                                  DteCollectionTimeLeft = s.DteCollectionTimeLeft,
                                  IsActive = s.IsActive,
                                  strRestrurantName = sd.strRestrurantName,
                                  strRestrurantType = sd.strRestrurantType,
                              }).ToListAsync();

            return View(data);
        }
        [HttpPost]
        public async Task<ActionResult> TakeFood(RestrurantFoodDto EmployeeID)
        {


            try
            {
                ResponseMessgeDto sub = new ResponseMessgeDto();
                var data = _context.TblRestrurantRows.Where(x => x.intRowId == EmployeeID.intRowId).FirstOrDefault();
                if (data != null)
                {
                    data.intEmployeeId = EmployeeID.intEmployeeId;
                    data.IsCollectReqest = true;

                    _context.TblRestrurantRows.AddOrUpdate(data);
                    await _context.SaveChangesAsync();

                    //Loging Employee Info to existing 
                    var log = _context.tblLogs.Where(x => x.intRestrurantId == EmployeeID.intRestrurantId && x.intRowid == EmployeeID.intRowId).FirstOrDefault();
                    if (log != null)
                    {

                        log.IntUserID = data.intEmployeeId;
                        _context.tblLogs.AddOrUpdate(log);
                        await _context.SaveChangesAsync();
                    }


                    sub.ResponseCode = 200;
                    sub.Message = "Added SucessFully";
                }
                else
                {
                    sub.ResponseCode = 400;
                    sub.Message = "Data Not Found";

                }

                return View(sub);

            }
            catch (Exception ex)
            {
                throw ex;
            }

           
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser(UserDto user)
        {

            try
            {
                ResponseMessgeDto sub = new ResponseMessgeDto();
                var data = _context.tblUsers.Where(x => x.intUserID == user.intUserID).FirstOrDefault();
                if (data != null)
                {
                    data.strUserType = user.strUserType;
                    data.strUserName = user.strUserName;
                    data.IsActive = user.IsActive;
                    data.intNgoID = user.intNgoID;
                    data.strUserMail = user.strUserMail;
                    data.strPassword = user.strPassword;

                    _context.tblUsers.AddOrUpdate(data);
                    await _context.SaveChangesAsync();

                    sub.ResponseCode = 200;
                    sub.Message = "updated SucessFully";
                }
                else
                {
                    tblUser ud = new tblUser
                    {
                        strUserType = user.strUserType,
                        strUserName = user.strUserName,
                        IsActive = user.IsActive,
                        intNgoID = user.intNgoID,
                        strUserMail = user.strUserMail,
                        strPassword = user.strPassword,

                    };
                    _context.tblUsers.AddOrUpdate(data);
                    await _context.SaveChangesAsync();

                    sub.ResponseCode = 200;
                    sub.Message = "Added SucessFully";
                }
                return View(sub);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }
        [HttpGet]

        public async Task<ActionResult> ViewUsers(long? userId)
        {

            var data = await (from us in _context.tblUsers
                              where us.intUserID == userId || userId == null
                              select new UserDto
                              {
                                  intUserID = us.intUserID,
                                  strUserType = us.strUserType,
                                  strUserName = us.strUserName,
                                  IsActive = us.IsActive,
                                  intNgoID = us.intNgoID,
                                  strUserMail = us.strUserMail,
                                  strPassword = us.strPassword,
                              }).ToListAsync();

            return View(data);
        }

    }
}