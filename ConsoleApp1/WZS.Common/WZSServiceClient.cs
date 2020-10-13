using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WZS.Common
{
    /// <summary>
    /// This client class can be used to send request the WZS.Net web services. This client will automatically parse your request and parameters to valid JSON and set it to the web service.
    /// The JSON response received from the WZS.Net web service will be deserialized and converted back to objects. This client also contains all possible methods/requests that you can use
    /// on to the WZS.Net web services.
    /// 
    /// Basically, the WZS.Net Service runs 2 controllers.
    /// 
    /// A Logincontroller (running on http(s)://:servername:port/api/login) 
    ///     
    ///     This controller is used to handle all logins, session setup, validation of users... etc. Basically all non-care home related calls.
    ///     All the calls referenced by the data controller require a SessionID parameter. This SessionID can be found in the LoginInfo result object after a successful login on the logincontroller. 
    ///     
    ///     This SessionID is kept on the server and links your session to the correct WZD
    ///     database, it stores your session's language information, it stores you user details... etc.
    ///     
    /// A Data Controller (running on http(s)://:servername:port/api/wzs) 
    /// 
    ///     This controller can be accessed once a session has been created by the logincontroller. It will provide access to the different WZD/Care Home databases that are actively linked to
    ///     the WZS.Net service. These database contains the actual care home data. (Residents, rooms, care plan...)
    /// </summary>
    public class WZSServiceClient : IDisposable
    {
        #region Internal properties
        private string baseurl;
        private HttpClient client = new HttpClient();
        private T PostTo<T>(string controller, dynamic json)
        {
            string postBody = JsonConvert.SerializeObject(json);
            HttpResponseMessage response = client.PostAsync(baseurl + "api/" + controller, new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
            CheckException(response);

            return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
        }
        private void PostTo(string controller, dynamic json)
        {
            string postBody = JsonConvert.SerializeObject(json);
            HttpResponseMessage response = client.PostAsync(baseurl + "api/" + controller, new StringContent(postBody, Encoding.UTF8, "application/json")).Result;
            CheckException(response);
        }
        private static void CheckException(HttpResponseMessage response)
        {
            var errorcode = (int)response.StatusCode;
            if (errorcode > 399 && errorcode < 500)
            {
                var error = JsonConvert.DeserializeObject<ErrorWithMessage>(response.Content.ReadAsStringAsync().Result);
                throw new ArgumentException(error.message);
            }
            else if (errorcode >= 500)
            {
                var error = JsonConvert.DeserializeObject<ErrorWithMessage>(response.Content.ReadAsStringAsync().Result);
                throw new Exception(error.message);
            }
        }
        private class ErrorWithMessage
        {
            public string message = null;
        }
        #endregion

        #region Constructor & Dispose methods
        /// <summary>
        /// Constructor for the WZSServiceClient.
        /// </summary>
        /// <param name="baseurl"> set to the url and port on which the WZS.Net service is running. (E.g. http://csmain.caresolutions.be:9000)</param>        
        /// <remarks name="cat">Constructor and Dispose methods</remarks>        
        public WZSServiceClient(string baseurl)
        {
            this.baseurl = baseurl;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        /// <summary>
        /// constructor        
        /// </summary>        
        public WZSServiceClient()
        {
            string baseurl = "csmain.caresolutions.be";
            this.baseurl = baseurl;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        /// <summary>
        /// Dispose method.
        /// </summary>
        /// <remarks name="cat">Constructor and Dispose methods</remarks>        
        public void Dispose()
        {
            client.Dispose();
        }
        #endregion

        #region Utilities and Helpers
        /// <summary>
        /// This request retrieves a list of Database images. All these images are stored in the DB as Base64 encoded JPEG files.
        /// A DBImage can be retrieved in 3 sizes: Tiny (50x50), small (150x150) and large (800x800)
        /// This method currently supports resident images (1 picture per resident) and wound images (multiple images per wound possible.)
        /// See method's parameters for more details.
        /// </summary>
        /// <param name="parms"> GetImagesParameters </param>
        /// <remarks name="cat"> Utilities and Helpers </remarks>
        /// <returns> L*DBImage </returns>
        public List<DBImage> GetImages(GetImagesParameters parms)
        {
            return PostTo<List<DBImage>>(controller: "wzs", json: new
            {
                method = "GetImages",
                @params = parms
            }
            );
        }
        /// <summary>
        /// This request can be used to retrieve a list of the status of a DBImage without the overhead of transferring the complete DBImage content.
        /// It will only return info on the image (ID, LinkID last changed date...) without the actual image content. (Also, returns the URL  - per image - of our internal image
        /// repository for direct download)
        /// 
        /// E.g. if you want to retrieve a list of all resident images changed after a certain date. You can use this call to see which images are changed, and next only get the 
        /// images you need directly by URL, or setting filters in the GetImagesParameters method.
        /// 
        /// </summary>
        /// <param name="parms"> GetImageStatusParameters </param>
        /// /// <remarks name="cat"> Utilities and Helpers </remarks>
        /// <returns> L*ImageStatus </returns>
        public List<ImageStatus> GetImageStatus(GetImageStatusParameters parms)
        {
            return PostTo<List<ImageStatus>>(controller: "wzs", json: new
            {
                method = "GetImageStatus",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Gets the text for a certain text ID.
        /// We store translations in a table. The different texts can be retrieved by the text's ID and a language ID.
        /// Using this method you can retrieve a specific translation for a certain text ID. See the GetTextParameters class for a description.)
        /// </summary>
        /// <param name="parms"> GetTextParameters </param>
        /// <remarks name="cat"> Utilities and Helpers </remarks>
        /// <returns> string</returns>
        public string GetText(GetTextParameters parms)
        {
            return PostTo<string>(controller: "wzs", json: new
            {
                method = "GetText",
                @params = parms
            }
            );
        }
        /// <summary>
        /// This method can be used to convert a DateTime value to its matching pair of CareDate and CareMomentID.
        /// E.g.: 12/01/2014 01:15AM can be translated to CareDate 11/01/2014 CareMomentID 6.
        /// 
        /// See CareMoment, DateTimeAsCareMoment and DateTimeToCaremomentParameters class for info
        /// </summary>
        /// <param name="parms">DateTimeToCaremomentParameters</param>
        /// <remarks name="cat">Utilities and Helpers</remarks>
        /// <returns>L*DateTimeAsCareMoment</returns>
        public List<DateTimeAsCareMoment> DateTimeToCaremoment(DateTimeToCaremomentParameters parms)
        {
            return PostTo<List<DateTimeAsCareMoment>>(controller: "wzs", json: new
            {
                method = "DateTimeToCaremoment",
                @params = parms
            });
        }
        /// <summary>
        /// Method to upload a DB image. (Source should be B64 encoded JPG)
        /// Can be used to upload a resident's image or a wound image.
        /// 
        /// Returns the unique ID value of the created image record.
        /// </summary>
        /// <param name="parms">UploadDBImageParameters</param>
        /// <remarks name="cat"> Utilities and Helpers </remarks>
        /// <returns>IDValue</returns>
        public IDValue UploadDBImage(UploadDBImageParameters parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "UploadDBImage",
                @params = parms
            });
        }
        /// <summary>
        /// Use this request to retrieve a list of objects and their last modification date.
        /// At the moment Resident ('SENIORS') object and  users ('USERS') modified dates can be retrieved.
        /// </summary>
        /// <param name="parms">GetModifiedObjectsParameters</param>
        /// <remarks name="cat"> Utilities and Helpers </remarks>
        /// <returns>L*ModifiedObject</returns>
        public List<ModifiedObject> GetModifiedObjects(GetModifiedObjectsParameters parms)
        {
            return PostTo<List<ModifiedObject>>(controller: "wzs", json: new
            {
                method = "GetModifiedObjects",
                @params = parms
            });
        }
        /// <summary>
        /// Use this request to retrieve a list of the different country codes used in the application.
        /// E.g. a countrycode ID is used to specify a Resident's nationality.
        /// </summary>
        /// <param name="parms">GetCountryCodesParameters</param>
        /// <remarks name="cat"> Utilities and Helpers </remarks>
        /// <returns>L*CountryCode</returns>
        public List<CountryCode> GetCountryCodes(GetCountryCodesParameters parms)
        {
            return PostTo<List<CountryCode>>(controller: "wzs", json: new
            {
                method = "GetCountryCodes",
                @params = parms
            });
        }

        /// <summary>
        /// Use this request to retrieve a list of LookupValues used in the application.
        /// See the LookUpValue returntype for more info.
        /// </summary>
        /// <param name="parms">GetLookupValuesParameters</param>
        /// <remarks name="cat"> Utilities and Helpers </remarks>
        /// <returns>L*LookupValue</returns>
        public List<LookupValue> GetLookUpValues(GetLookupValuesParameters parms)
        {
            return PostTo<List<LookupValue>>(controller: "wzs", json: new
            {
                method = "GetLookUpValues",
                @params = parms
            });
        }
        #endregion helpers

        #region Login and User Validation

        /// <summary>
        /// Basic Login method.
        /// Default way to set up a user session on the logincontroller. Does not use validated users and expects a WZDatabase suffix after the username.
        /// (See the LoginParams object for more info on these suffixes)
        /// </summary>
        /// <param name="parms"> LoginParams </param>
        /// <remarks name="cat">Login and User Validation</remarks>
        /// <returns> LoginInfo </returns>
        public LoginInfo Login(LoginParams parms)
        {
            return PostTo<LoginInfo>(controller: "login", json: new
            {
                method = "login",
                @params = parms
            });
        }
        /// <summary>
        /// Request to list all linked WZDDabases your session has access to (SconDB), this access is defined by your application key. On installation, CS can activate your application
        /// key on each linked WZD Database independently. Each Care Home has its own database. A session is set up and linked to a WZDatabase.
        /// If you want to login to a different database. You will need login again and create a new session. (See Login method)
        /// </summary>
        /// <param name="parms"> GetWZDatabasesParams </param>
        /// <remarks name="cat">Login and User Validation</remarks>
        /// <returns> L*SconDB </returns>
        public List<SconDB> GetWZDatabases(GetWZDatabasesParams parms)
        {
            return PostTo<List<SconDB>>(controller: "login", json: new
            {
                method = "GetWZDatabases",
                @params = parms
            });
        }
        /// <summary>
        /// Request to login an active user by using an externally defined ID. (E.g. Badge number, iButton,... on a room terminal.)
        /// This method can be used to authenticate a user using an external ID, no password is necessary. In order to use this method successfully, a user needs to have this external
        /// ID configured in the CS Software's user management. In this screen, user's can enter a badge number, iButton code... that can be used to reference this user. Only 1 code
        /// per user is possible.
        /// 
        /// This method does not require a password and automatically validates a user if a validated user for you application does not exist. So ne ValidateUser call is necessary.
        /// 
        /// The system will look for a user with a matching external ID in the selected care home database, and (if found) will initiate a WZS.Net session for this user and will return
        /// a valid LoginInfo object for this session.
        /// </summary>
        /// <param name="parms"> LoginUserByExtIdParams </param>
        /// <remarks name="cat">Login and User Validation</remarks>
        /// <returns> LoginInfo </returns>
        public LoginInfo LoginUserByExtId(LoginUserByExtIdParams parms)
        {
            return PostTo<LoginInfo>(controller: "login", json: new
            {
                method = "LoginUserByExtId",
                @params = parms
            });
        }
        /// <summary>
        /// Request to login an active, validated user by using the user's email address as defined in the CS application, without entering a username of password. 
        /// (Can be a user, or a resident's contact.) In order to use this method successfully, a user needs to have the correct email configured the CS Software's user management.        
        ///
        /// Because no username and password are required, we request that the user is validated for use with your application key. You can validate a user with the 'ValidateUserByEmail'
        /// method. Once a user account has been validated, you can use the LoginValidatedUserByEmail to initiate a session without entering a username or password.
        /// Users need to be validated per database and per application key. If a user account's record changes (user changes password, name, active state…) in our application, the user
        /// will need a new validation. (This is to prevent setting up sessions with inactive users, old user passwords etc...)
        ///
        /// The system will look for a validated user with a matching email address in the selected care home database, and (if found) will initiate a WZS.Net session for this user and will return
        /// a valid LoginInfo object for this session.
        /// </summary>
        /// <param name="parms"> LoginValidatedUserByEmailParams </param>
        /// <remarks name="cat">Login and User Validation</remarks>
        /// <returns> LoginInfo </returns>
        public LoginInfo LoginValidatedUserByEmail(LoginValidatedUserByEmailParams parms)
        {
            return PostTo<LoginInfo>(controller: "login", json: new
            {
                method = "LoginValidatedUserByEmail",
                @params = parms
            });
        }
        /// <summary>
        /// This method can be used to validate a user for your application key, for a certain WZDatabase. Once a user is validated, you can setup a session with this user account
        /// without having to enter this user's CS username or CS Password. (only for the validated database) This can be useful if you want your users to transparently log on to our 
        /// web service, without them having to enter their CS username and password each time. (Allows you to use your own passwords)
        /// 
        /// Validation requires the user's CS email and CS password to be entered once. After a user is successfully validated once, you can use the LoginValidatedUserByEmail request
        /// to log in this user on a set WZDatabase without entering a password.
        /// 
        /// As soon as the user changes his name/password/active state in CS software, the validation record will automatically be removed. So, in order to login using 
        /// LoginValidatedUserByEmail again, you will need to issue another ValidateUserByEmail request first.
        /// </summary>
        /// <param name="parms"> LoginValidatedUserByEmailParams </param>
        /// <remarks name="cat">Login and User Validation</remarks>
        /// <returns> LoginInfo </returns>
        public bool ValidateUserByEmail(ValidateUserByEmailParams parms)
        {
            return PostTo<bool>(controller: "login", json: new
            {
                method = "ValidateUserByEmail",
                @params = parms
            });
        }
        /// <summary>
        /// Request to login an active, validated user by using the user's username/initials as defined in the CS application, without entering a password. 
        ///
        /// Because no password is required, we request that the user is validated for use with your application key. You can validate a user with the 'ValidateUserByUserName'
        /// method. Once a user account has been validated, you can use the LoginValidatedUserByUsername method to initiate a session without entering a username or password.
        /// Users need to be validated per database and per application key. If a user account's record changes (user changes password, name, active state...) in our application, the user
        /// will need a new validation. (This is to prevent setting up sessions with inactive users, old user passwords etc...)
        ///
        /// The system will look for a validated user with a matching username in the selected care home database, and (if found) will initiate a WZS.Net session for this user and will return
        /// a valid LoginInfo object for this session.
        /// </summary>
        /// <param name="parms"></param>
        /// <remarks name="cat">Login and User Validation</remarks>
        /// <returns></returns>
        public LoginInfo LoginValidatedUserByUsername(LoginValidatedUserByUserNameParams parms)
        {
            return PostTo<LoginInfo>(controller: "login", json: new
            {
                method = "LoginValidatedUserByUsername",
                @params = parms
            });
        }
        /// <summary>
        /// This method can be used to validate a user for your application key, for a certain WZDatabase. Once a user is validated, you can setup a session with this user account
        /// without having to enter this user's CS username or CS Password. (only for the validated database) This can be useful if you want your users to transparently log on to our 
        /// web service, without them having to enter their CS username and password each time. (Allows you to use your own passwords)
        /// 
        /// Validation requires the user's CS username and CS password to be entered once. After a user is successfully validated once, you can use the LoginValidatedUserByUsername
        /// request to log in this user on a set WZDatabase without entering a password.
        /// 
        /// As soon as the user changes his name/password/active state in CS software, the validation record will automatically be removed. So, in order to login using 
        /// LoginValidatedUserByUsername again, you will need to issue another ValidatUserByUsername request first.
        /// </summary>
        /// <param name="parms"> ValidateUserByUserNameParams </param>
        /// <remarks name="cat">Login and User Validation</remarks>
        /// <returns> LoginInfo </returns>
        public bool ValidateUserByUserName(ValidateUserByUserNameParams parms)
        {
            return PostTo<bool>(controller: "login", json: new
            {
                method = "ValidateUserByUserName",
                @params = parms
            });
        }
        /// <summary>
        /// This method can be used to list all linked WZDatabases's Displayname. Can be used without a SessionID. (Now superseded by GetWZDatabases method, however still available.)
        /// </summary>
        /// <remarks name="cat">Login and User Validation</remarks>
        /// <returns> L*string </returns>
        public List<string> Connections()
        {
            return PostTo<List<string>>(controller: "Login", json: new
            {
                method = "connections"
            });
        }

        #endregion

        #region Rooms and Departments
        /// <summary>
        /// Use this request to retrieve a list of Departments defined in the Care Home you are connected to.
        /// </summary>
        /// <param name="parms">GetDepartmentListParameters</param>
        /// <remarks name="cat">Rooms and Departments</remarks>
        /// <returns>L*Department</returns>
        public List<Department> GetDepartmentList(GetDepartmentListParameters parms)
        {
            return PostTo<List<Department>>(controller: "wzs", json: new
            {
                method = "GetDepartmentList",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to retrieve a list of the different rooms in this Care Home. Can be filtered. See Parameters.
        /// </summary>
        /// <param name="parms">GetRoomListParameters</param>
        /// <remarks name="cat">Rooms and Departments</remarks>
        /// <returns>L*Room</returns>
        public List<Room> GetRoomList(GetRoomListParameters parms)
        {
            return PostTo<List<Room>>(controller: "wzs", json: new
            {
                method = "GetRoomList",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to retrieve the occupancy per room, per date. Method returns a record for each date per room.
        /// Displays room max capacity, and actual occupied capacity. Can be filtered.
        /// </summary>
        /// <param name="parms">GetRoomOccupancyParams</param>
        /// <remarks name="cat">Rooms and Departments</remarks>
        /// <returns>L*RoomOccupancyStatus</returns>
        public List<RoomOccupancyStatus> GetRoomOccupancy(GetRoomOccupancyParams parms)
        {
            return PostTo<List<RoomOccupancyStatus>>(controller: "wzs", json: new
            {
                method = "GetRoomOccupancy",
                @params = parms
            });
        }
        /// <summary>
        /// Method to retrieve the occupancy per short stay room  per date. Some rooms can be rented out on a day to day basis, these rooms are marked as Short Stay rooms in the application.)
        /// Method returns a record for each date per room. Shown in the method's result is the room max capacity, and actual occupied capacity. Can be filtered.
        /// </summary>
        /// <param name="parms">GetRoomOccupancyParams</param>
        /// <remarks name="cat">Rooms and Departments</remarks>
        /// <returns>L*RoomOccupancyStatus</returns>
        public List<RoomOccupancyStatus> GetShortStayRoomOccupancy(GetRoomOccupancyParams parms)
        {
            return PostTo<List<RoomOccupancyStatus>>(controller: "wzs", json: new
            {
                method = "GetShortStayRoomOccupancy",
                @params = parms
            });
        }
        /// <summary>
        /// Method to retrieve a list of the available Roomtypes.
        /// </summary>
        /// <param name="parms">GetRoomtypesParams</param>
        /// <remarks name="cat">Rooms and Departments</remarks>
        /// <returns>L*Roomtype</returns>
        public List<Roomtype> GetRoomtypes(GetRoomtypesParams parms)
        {
            return PostTo<List<Roomtype>>(controller: "wzs", json: new
            {
                method = "GetRoomtypes",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to retrieve a list of the available Roomstyles.
        /// </summary>
        /// <param name="parms">GetRoomstylesParams</param>
        /// <remarks name="cat">Rooms and Departments</remarks>
        /// <returns>L*Roomstyle</returns>
        public List<Roomstyle> GetRoomstyles(GetRoomstylesParams parms)
        {
            return PostTo<List<Roomstyle>>(controller: "wzs", json: new
            {
                method = "GetRoomstyles",
                @params = parms
            }
            );
        }
      
        #endregion

        #region Residents and Resident Info
        /// <summary>
        /// Method to retrieve the list of residents in the Care Home. Can be filtered.
        /// </summary>
        /// <param name="parms">GetResidentsParameters</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        /// <returns>L*Resident</returns>
        public List<Resident> GetResidents(GetResidentsParameters parms)
        {
            return PostTo<List<Resident>>(controller: "wzs", json: new
            {
                method = "GetResidents",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to retrieve the contact persons and their details for a single resident.
        /// </summary>
        /// <param name="parms">GetContactsForResidentParameters</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        /// <returns>L*Contact</returns>
        public List<Contact> GetContactsForResident(GetContactsForResidentParameters parms)
        {
            return PostTo<List<Contact>>(controller: "wzs", json: new
            {
                method = "GetContactsForResident",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method retrieves the list of all current and atchived diseases. Contracted by all residents, or a specific resident.
        /// Can be filtered.
        /// </summary>
        /// <param name="parms">GetResidentDiseasesParameters</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        /// <returns>L*ResidentDisease</returns>
        public List<ResidentDisease> GetResidentDiseases(GetResidentDiseasesParameters parms)
        {
            return PostTo<List<ResidentDisease>>(controller: "wzs", json: new
            {
                method = "GetResidentDiseases",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method retrieves a list of general medical remarks for a single resident or all residents.
        /// The remarks include: allergies, doctor details, diabetic info... etc. Check the ResidentMedicalRemark class for details.
        /// Can be filtered.
        /// </summary>
        /// <param name="parms">GetResidentMedicalRemarksParameters</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        /// <returns>L*ResidentMedicalRemark_v2</returns>
        [Obsolete("GetResidentMedicalRemarks is deprecated due to a bug in Rhesus, please use GetResidentMedicalRemarks_v2 instead.")]
        public List<ResidentMedicalRemark_v2> GetResidentMedicalRemarks(GetResidentMedicalRemarksParameters parms)
        {
            return PostTo<List<ResidentMedicalRemark_v2>>(controller: "wzs", json: new
            {
                method = "GetResidentMedicalRemarks",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method retrieves a list of general medical remarks for a single resident or all residents.
        /// The remarks include: allergies, doctor details, diabetic info... etc. Check the ResidentMedicalRemark class for details.
        /// Can be filtered.
        /// </summary>
        /// <param name="parms">GetResidentMedicalRemarksParameters</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        /// <returns>L*ResidentMedicalRemark_v2</returns>
        public List<ResidentMedicalRemark_v2> GetResidentMedicalRemarks_v2(GetResidentMedicalRemarksParameters parms)
        {
            return PostTo<List<ResidentMedicalRemark_v2>>(controller: "wzs", json: new
            {
                method = "GetResidentMedicalRemarks_v2",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method retrieves a list of parameter values that were recorded for a single resident or a list of residents.
        /// (Includes; registered temperature values, blood pressure values, pulse...)
        /// See the ParamList, ParamDetails and ResidentParameter class.
        ///    
        /// Can be filtered.
        /// </summary>
        /// <param name="parms">GetResidentParamsParameters</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        /// <returns>L*ResidentParameter</returns>
        public List<ResidentParameter> GetResidentParams(GetResidentParamsParameters parms)
        {
            return PostTo<List<ResidentParameter>>(controller: "wzs", json: new
            {
                method = "GetResidentParams",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to retrieve a list of the TreatmentDirectives for a resident.
        /// A treatment directive describes what precautions/actions a resident has requested, agreed or not agreed to in case of hospitalization, severe sickness or injury...
        /// Eg. describes if a resident still want's to have CPO administered, still wants to be treated in the hospital, ...
        /// </summary>
        /// <param name="parms">GetResidentTreatmentDirectivesParameters</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        /// <returns>L*ResidentTreatmentDirective</returns>
        public List<ResidentTreatmentDirective> GetResidentTreatmentDirectives(GetResidentTreatmentDirectivesParameters parms)
        {
            return PostTo<List<ResidentTreatmentDirective>>(controller: "wzs", json: new
            {
                method = "GetResidentTreatmentDirectives",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method used to register a Katz Score for a resident.
        /// </summary>
        /// <param name="parms">InsertKatzParameters</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        public void InsertKatz(InsertKatzParameters parms)
        {
            PostTo(controller: "wzs", json: new
            {
                method = "InsertKatz",
                @params = parms
            });
        }
        /// <summary>
        /// Method to list the vaccinations a resident (or a list of residents) has received.
        /// Can be filtered.
        /// </summary>
        /// <param name="parms">GetResidentVaccinationsParams</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        /// <returns>L*ResidentVaccination</returns>
        public List<ResidentVaccination> GetResidentVaccinations(GetResidentVaccinationsParams parms)
        {
            return PostTo<List<ResidentVaccination>>(controller: "wzs", json: new
            {
                method = "GetResidentVaccinations",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to register a vaccination a Resident has received.
        /// Returns the unique ID value of the created ResidentVaccination record.
        /// </summary>
        /// <param name="parms">InsertResidentVaccinationParams</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        /// <returns>IDValue</returns>
        public IDValue InsertResidentVaccination(InsertResidentVaccinationParams parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "InsertResidentVaccination",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to retrieve a list of available Resident DocumentTypes. Every resident document you upload using
        /// the InsertResidentDocument method has to have a Resident Document Type set.
        /// </summary>
        /// <param name="parms">GetResidentDocumentTypesParameters</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        /// <returns>L*ResidentDocumentType</returns>
        public List<ResidentDocumentType> GetResidentDocumentTypes(GetResidentDocumentTypesParameters parms)
        {
            return PostTo<List<ResidentDocumentType>>(controller: "wzs", json: new
            {
                method = "GetResidentDocumentTypes",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to upload an external document into the resident's linked document's list.
        /// Returns the unique ID value of the created DocumentLink record. The uploaded file will be saved
        /// in the WZD Document's directory. The documents will be made available to the users of WZD when reviewing the resident's file.
        /// Returns the internal filename used by the application. (GUID + extension)
        /// </summary>
        /// <param name="parms">InsertResidentDocumentParameters</param>
        /// <remarks name="cat">Residents and Resident Info</remarks>
        /// <returns>string</returns>
        public string InsertResidentDocument(InsertResidentDocumentParameters parms)
        {
            return PostTo<string>(controller: "wzs", json: new
            {
                method = "InsertResidentDocument",
                @params = parms
            });
        }
        #endregion

        #region Doctors and Treatment
        /// <summary>
        /// Method used to retrieve a list of doctor visits that were registered in the CS Software.
        /// Can only be used by a Session set up for a user ID that is defined as - or linked to a doctor in the CS Software. Else no results will be retrieved.
        /// 
        /// A Doctor will only receive his/her own visits. No visits from other doctors are visible.
        /// 
        /// Can be filtered.
        /// </summary>
        /// <param name="parms">GetDoctorVisitsParameters</param>
        /// <remarks name="cat">Doctors and Treatment</remarks>
        /// <returns>L*DoctorVisit</returns>
        public List<DoctorVisit> GetDoctorVisits(GetDoctorVisitsParameters parms)
        {
            return PostTo<List<DoctorVisit>>(controller: "wzs", json: new
            {
                method = "GetDoctorVisits",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to retrieve known nomenclature, used when registering doctor/kine visits.
        /// Defines the different type of doctorvists that are possible, also defines standard rate of such a visit.
        /// 
        /// Can be filtered.
        /// </summary>
        /// <param name="parms">GetNomenclatureParameters</param>
        /// <remarks name="cat">Doctors and Treatment</remarks>
        /// <returns>L*NomenclatureWithRateList</returns>
        public List<NomenclatureWithRateList> GetNomenClature(GetNomenclatureParameters parms)
        {
            return PostTo<List<NomenclatureWithRateList>>(controller: "wzs", json: new
            {
                method = "GetNomenclature",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to register a single doctors visit for a resident.
        /// You can set the rate you want to be invoiced to the resident. (if applicable.)
        /// 
        /// A doctor can only register visits for his/her own residents.
        /// 
        /// Returns the ID Value of the created visit.
        /// </summary>
        /// <param name="parms">InsertDoctorVisitParameters</param>
        /// <remarks name="cat">Doctors and Treatment</remarks>
        /// <returns>IDValue</returns>
        public IDValue InsertDoctorVisit(InsertDoctorVisitParameters parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "InsertDoctorVisit",
                @params = parms
            });
        }
        /// <summary>
        /// Method to remove a registered DoctorVisit. A doctor can only remove his/her own visits.
        /// Visit can not be invoiced yet.
        /// </summary>
        /// <param name="parms">RemoveDoctorVisitsParameters</param>
        /// <remarks name="cat">Doctors and Treatment</remarks>
        public void RemoveDoctorVisit(RemoveDoctorVisitsParameters parms)
        {
            PostTo(controller: "wzs", json: new
            {
                method = "RemoveDoctorVisit",
                @params = parms
            });
        }
        #endregion

        #region Registrations, parameters and observations
        /// <summary>
        /// Method to retrieve all the possible parameter types that can be registered and measured for a resident. (E.g. for use with InsertParam etc...)
        /// E.g. Blood pressure value, temperature, weight... etc.
        /// 
        /// Check the Parameter, ParamDetail and ResidentParameter class.
        /// 
        /// Can be filtered.
        /// </summary>
        /// <param name="parms">GetParamListParameters</param>
        /// <remarks name="cat">Registrations, parameters and observations</remarks>
        /// <returns>L*Parameter</returns>
        public List<Parameter> GetParamList(GetParamListParameters parms)
        {
            return PostTo<List<Parameter>>(controller: "wzs", json: new
            {
                method = "GetParamList",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to retrieve all the possible parameter detail types. 
        /// (fixed list of secondary values to choose from when registering a parameter.)
        ///
        /// Some parameter require a secondary value to be registered to further specify the measured parameter.
        /// E.g. For a pulse,  you can register the ppm (value) but also whether this pulse was weak, strong, irregular.... (value2)
        /// Check the Parameter, ParamDetail and ResidentParameter class.
        /// 
        /// Can be filtered.
        /// </summary>
        /// <param name="parms">GetParamDetailParameters</param>
        /// <remarks name="cat">Registrations, parameters and observations</remarks>
        /// <returns>L*ParamDetail</returns>
        public List<ParamDetail> GetParamDetails(GetParamDetailParameters parms)
        {
            return PostTo<List<ParamDetail>>(controller: "wzs", json: new
            {
                method = "GetParamDetails",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method used to retrieve all possible care-actions/tasks a user can register in the CS Software. E.g. wash a resident, help cloth a resident, take a parameter,... etc)
        /// These actions are made up of a combination of hardcoded actionlist items. (ActionListID smaller or equal to 60) These actionlist items exist on all CS installations. A user can also add
        /// custom actions. (ActionListID higher then 60) These differ from db to db.
        /// 
        /// These actionlist items are used to create registration and observations with. The will also be used a resident's care planning.
        /// 
        /// This list is returned as a tree view using parent ID’s. CanRegister property of ActionList class defines if the action type can be used to register a task. If this property is
        /// null, it is simply a group header used in the tree view. See the ActionList class for details.
        /// </summary>
        /// <param name="parms">GetActionListParameters</param>
        /// <remarks name="cat">Registrations, parameters and observations</remarks>
        /// <returns>L*ActionList</returns>
        public List<ActionList> GetActionList(GetActionListParameters parms)
        {
            return PostTo<List<ActionList>>(controller: "wzs", json: new
            {
                method = "GetActionList",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method used to register a measured parameter for a resident. (not originating from a care plan, use SignPlannedParam for those.)
        /// See GetParamList, GetParamDetails, InsertParamParameters for details.
        /// </summary>
        /// <param name="parms">InsertParamParameters</param>
        /// <remarks name="cat">Registrations, parameters and observations</remarks>
        public void InsertParam(InsertParamParameters parms)
        {
            PostTo(controller: "wzs", json: new
            {
                method = "InsertParam",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to create an observation for a resident. An observation is a task that you register while specifying additional remarks or details.
        /// This text (required) is then registered alongside the task. You can link these observations to the different modules in the CS software. If you do, the entered text
        /// will be visible in those modules.
        /// 
        /// E.g. if you want register the action 'help clothing a resident' and you notice that this resident's has a small wound. You can use CreateObservation with ActionList
        /// type 4 ('Help with clothing'), and remarks: 'Resident's has a cut on left leg.' and link this observation (using modlinkables) to the Wounds module.
        /// User's of the Wounds module will see that you noticed a wound during clothing, and can react accordingly.
        /// 
        /// You can also use CreateObservation with ActionList type 22 (Common observation) to send general messages to the different communication modules.
        /// 
        /// In general, CreateObservation is used when you wish to add remarks to/while registering a  task. (Registration)
        /// </summary>
        /// <param name="parms">CreateObservationParameters</param>
        /// <remarks name="cat">Registrations, parameters and observations</remarks>
        public void CreateObservation(CreateObservationParameters parms)
        {
            PostTo(controller: "wzs", json: new
            {
                method = "CreateObservation",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to register/sign a non-planned (ActionList) caretask or action that was executed for a resident.
        /// See ActionList for details.
        /// See SignPlannedTask if you wish to sign planned actions/caretasks. 
        /// 
        /// See CreateRegistrationParameters for details.
        ///
        /// </summary>
        /// <param name="parms">CreateRegistrationParameters</param>
        /// <remarks name="cat">Registrations, parameters and observations</remarks>
        public void CreateRegistration(CreateRegistrationParameters parms)
        {
            PostTo(controller: "wzs", json: new
            {
                method = "CreateRegistration",
                @params = parms
            });
        }
        #endregion

        #region Care Planning
        /// <summary>
        /// Method used to sign a planned task for a resident, received through his/her careplan as executed. (See GetCarePlanForResident)
        /// A sign action is linked to user ID. It is permitted for a planned task to be signed multiple time, but by different users.
        /// Note that you will need the TaskLinkID from the GetCarePlanForResident method. This links the registered Action to the generated planning.
        /// </summary>
        /// <param name="parms">SignPlannedTaskParameters</param>
        /// <remarks name="cat">Care Planning</remarks>
        public void SignPlannedTask(SignPlannedTaskParameters parms)
        {
            PostTo(controller: "wzs", json: new
            {
                method = "SignPlannedTask",
                @params = parms
            });
        }
        /// <summary>
        /// Method to signs a planned task as not executed. You have the option to specify a reason why the task was not executed.
        /// This call can be used if a user was prevented from executing said task.
        /// 
        /// E.g. if a user task is planned to help a resident put on his glasses, but the glasses could not be found.
        /// The user can then sign this task as not executed with remarks: "Glasses were nowhere to be found."
        /// </summary>
        /// <param name="parms">SignPlannedTaskNotDoneParameters</param>
        /// <remarks name="cat">Care Planning</remarks>
        public void SignPlannedTaskNotDone(SignPlannedTaskNotDoneParameters parms)
        {
            PostTo(controller: "wzs", json: new
            {
                method = "SignPlannedTaskNotDone",
                @params = parms
            });
        }
        /// <summary>
        /// Method to retrieve a list of caremoments as configured in the CS Software.
        /// See the CareMoment class for info.
        /// </summary>
        /// <param name="parms">GetCareMomentsParameters</param>
        /// <remarks name="cat">Care Planning</remarks>
        /// <returns>L*CareMoment</returns>
        public List<CareMoment> GetCareMoments(GetCareMomentsParameters parms)
        {
            return PostTo<List<CareMoment>>(controller: "wzs", json: new
            {
                method = "GetCareMoments",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to retrieve the planned care tasks for a resident. (Or a set of residents.)
        /// 
        /// This planning reflects all the planned tasks and parameters that need to be executed per resident, per caremoment.
        /// This list is generated by all the modules in the CS Software. (Comfort planning, wounds treatment, parameter measurement... etc.)
        /// It can contain information items (not to be signed, purely for info).
        /// Some items can only be signed by a nurse. (Diabetic's glycaemia...) These items will have the 'IsNurseAction' property set to true.
        /// Use SignPlannedTask and SignPlannedParam to register the items of the Care Plan as executed.
        /// 
        /// This planning DOES NOT include medication.
        /// 
        /// Can be filtered.
        /// </summary>
        /// <param name="parms">GetCarePlanForResidentParameters</param>
        /// <remarks name="cat">Care Planning</remarks>
        /// <returns>L*CarePlanItem</returns>
        public List<CarePlanItem> GetCarePlanForResident(GetCarePlanForResidentParameters parms)
        {
            return PostTo<List<CarePlanItem>>(controller: "wzs", json: new
            {
                method = "GetCarePlanForResident",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method used to sign a planned parameter for a resident, received through his/her care plan as measured. (See GetCarePlanForResident)
        /// A sign action is linked to user ID. It is permitted for a planned task to be signed multiple time, but by different users.
        /// Note that you will need the TaskLinkID from the GetCarePlanForResident method. This links the registered Parameter to the generated planning.
        /// If you use InsertParam to register a parameter on a care plan the application will not be able to link the measured value to the resident's generated
        /// planning. So please use SingPlannedParam to sign parameters from the Care Plan.
        /// </summary>
        /// <param name="parms">SignPlannedParamParameters</param>
        /// <remarks name="cat">Care Planning</remarks>
        public void SignPlannedParam(SignPlannedParamParameters parms)
        {
            PostTo(controller: "wzs", json: new
            {
                method = "SignPlannedParam",
                @params = parms
            });
        }
        #endregion

        #region Communication
        /// <summary>
        /// Method to retrieve a list of all the appointments on a single resident's (or all residents) Resident Calendar for a set period in time.
        /// Can be filtered.
        /// </summary>
        /// <param name="parms">GetAppointmentsParameters</param>
        /// <remarks name="cat">Communication</remarks>
        /// <returns>L*Appointment</returns>
        public List<Appointment> GetAppointments(GetAppointmentsParameters parms)
        {
            return PostTo<List<Appointment>>(controller: "wzs", json: new
            {
                method = "GetAppointments",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method used to retrieve all the different Appointment Types used in the application. (An appointment must be created with a certain type.)
        /// E.g. Visit family, visit hairdresser...
        /// 
        /// This list of appointmenttypes can be customized by the user. However type 1 (Animation) and type 2 (Planning Kine) are hardcoded in the application and 
        /// will always be available.
        /// </summary>
        /// <param name="parms">GetAppointmentTypesParameters</param>
        /// <remarks name="cat">Communication</remarks>
        /// <returns>L*AppointmentType</returns>
        public List<AppointmentType> GetAppointmentTypes(GetAppointmentTypesParameters parms)
        {
            return PostTo<List<AppointmentType>>(controller: "wzs", json: new
            {
                method = "GetAppointmentTypes",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to retrieve all communication messages (e.g. observations) sent to a CS Software module or Communications Module.  (See GetModuleList)
        /// The items read/unread status is kept per user.
        /// 
        /// e.g. A doctor can use this method to retrieve all unread messages sent to the 'Doctor's communication channel' in WZD. He can read his messages and mark
        /// them as read. In a few days, if he logs in again, he can check for new messages and repeat the above.
        /// 
        /// At the moment this is used for the communication channel between CS and a resident's doctor. And for the communication channel between CS and a Resident's family.
        /// </summary>
        /// <param name="parms">GetCommunicationItemsParameters</param>
        /// <remarks name="cat">Communication</remarks>
        /// <returns>L*CommunicationItem</returns>
        public List<CommunicationItem> GetCommunicationItems(GetCommunicationItemsParameters parms)
        {
            return PostTo<List<CommunicationItem>>(controller: "wzs", json: new
            {
                method = "GetCommunicationItems",
                @params = parms
            }
            );
        }
        /// <summary>
        /// !!DEPRECATED!! Please use GetDiary_V2
        /// Method used to retrieve all the actions/remarks/communication that was sent to the Care Home's Diary. (Or Day/Night book)
        /// Can be filtered by resident ID, and begin/end date.
        /// </summary>
        /// <param name="parms">GetDiaryParameters</param>
        /// <remarks name="cat">Communication</remarks>
        /// <returns>L*Diary</returns>
        [Obsolete("GetDiary is deprecated, please use GetDiary_v2 instead. (Addition of general remarks and department filter")]
        public List<Diary> GetDiary(GetDiaryParameters parms)
        {
            return PostTo<List<Diary>>(controller: "wzs", json: new
            {
                method = "GetDiary",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method used to retrieve all the actions/remarks/communication that was sent to the Care Home's Diary. (Or Day/Night book)
        /// Can be filtered by resident ID, and begin/end date. Can be set to include General (non-resident specific) items or only Resident 
        /// specific item. Or both.
        /// </summary>
        /// <param name="parms">GetDiaryParameters_v2</param>
        /// <remarks name="cat">Communication</remarks>
        /// <returns>L*Diary_v2</returns>
        public List<Diary_v2> GetDiary_v2(GetDiaryParameters_v2 parms)
        {
            return PostTo<List<Diary_v2>>(controller: "wzs", json: new
            {
                method = "GetDiary_v2",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Retrieves a list of Modules, used in the CS Software. (See CarePlanItem, Modlinkables classes...)
        /// Each task/item is part of a module. At the moment this list list 2 groups of modules. 1 for general actions/tasks etc... and 1 group
        /// containing the different communication channels. You can use these modules to link Observations to.
        /// </summary>
        /// <param name="parms">GetModulesParameters</param>
        /// <remarks name="cat">Communication</remarks>
        /// <returns>L*Module</returns>
        public List<Module> GetModules(GetModulesParameters parms)
        {
            return PostTo<List<Module>>(controller: "wzs", json: new
            {
                method = "GetModules",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method used to add an appointment to a resident's calendar. You have the option to display these appointments (as informational) item on the Resident's Care
        /// Plan and the Care Home's Diary.
        /// 
        /// See CreateAppointmentParameters for more info.
        /// </summary>
        /// <param name="parms">CreateAppointmentParameters</param>
        /// <remarks name="cat">Communication</remarks>
        public void CreateAppointment(CreateAppointmentParameters parms)
        {
            PostTo(controller: "wzs", json: new
            {
                method = "CreateAppointment",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to mark a single communication Item as read. (See GetCommunicationItems)
        /// </summary>
        /// <param name="parms">MarkCommunicationAsReadParameters</param>
        /// <remarks name="cat">Communication</remarks>
        public void MarkCommunicationAsRead(MarkCommunicationAsReadParameters parms)
        {
            PostTo(controller: "wzs", json: new
            {
                method = "MarkCommunicationAsRead",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to mark a set (or all) your communication Item as read. (See GetCommunicationItems)
        /// </summary>
        /// <param name="parms">MarkAllCommunicationAsReadParameters</param>
        /// <remarks name="cat">Communication</remarks>
        public int MarkAllCommunicationAsRead(MarkAllCommunicationAsReadParameters parms)
        {
            return PostTo<int>(controller: "wzs", json: new
            {
                method = "MarkAllCommunicationAsRead",
                @params = parms
            });
        }
        /// <summary>
        /// Method to respond to a communication item. This item will be linked to the original communicationitem, and will include this original item's text.
        /// As such forming a complete communication string equal to a mail string etc...
        /// 
        /// Returns the ID as int for the inserted communication item
        /// </summary>
        /// <param name="parms">RespondToCommunicationParameters</param>
        /// <remarks name="cat">Communication</remarks>
        /// <returns>int</returns>
        public int RespondToCommunication(RespondToCommunicationParameters parms)
        {
            return PostTo<int>(controller: "wzs", json: new
            {
                method = "RespondToCommunication",
                @params = parms
            });
        }
        /// <summary>
        /// Method to retrieve a list of all possible modlinkables. A 'Modlinkable' can be used to link a Created Observation and make its message appear
        /// in the set CS module. See Modlinkable class for details. 
        /// </summary>
        /// <param name="parms">GetModLinkablesParameters</param>
        /// <remarks name="cat">Communication</remarks>
        /// <returns>ModLinkable</returns>
        public List<ModLinkable> GetModLinkables(GetModLinkablesParameters parms)
        {
            return PostTo<List<ModLinkable>>(controller: "wzs", json: new
            {
                method = "GetModlinkables",
                @params = parms
            });
        }
        #endregion

        #region Call System
        /// <summary>
        /// Method used to retrieve a call object by external ID.
        /// This ID is the ID that is used by the third party call system.
        /// 
        /// Returns a list containing 1 call object.
        /// </summary>
        /// <param name="parms">GetCallByIDParameters</param>
        /// <remarks name="cat">Call System</remarks>
        /// <returns>L*Call</returns>
        public List<Call> GetCallByID(GetCallByIDParameters parms)
        {
            return PostTo<List<Call>>(controller: "wzs", json: new
            {
                method = "GetCallByID",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method used to retrieve a call object by external ID.
        /// This ID is the ID that is used by the third party call system.
        /// 
        /// Returns a list containing 1 call object.
        /// 
        /// v2: returns new call_v2 type.
        /// </summary>
        /// <param name="parms">GetCallByIDParameters</param>
        /// <remarks name="cat">Call System</remarks>
        /// <returns>L*Call_v2</returns>
        public List<Call_v2> GetCallByID_v2(GetCallByIDParameters parms)
        {
            return PostTo<List<Call_v2>>(controller: "wzs", json: new
            {
                method = "GetCallByID_v2",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method used to retrieve a list of call objects for a single resident (or all residents)
        /// for a specified date time range.
        /// See GetCallsForResidentParameters and Call class for details.
        /// </summary>
        /// <param name="parms">GetCallsForResidentParameters</param>
        /// <remarks name="cat">Call System</remarks>
        /// <returns>L*Call</returns>
        public List<Call> GetCallsForResident(GetCallsForResidentParameters parms)
        {
            return PostTo<List<Call>>(controller: "wzs", json: new
            {
                method = "GetCallsForResident",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method used to retrieve a list of call objects for a single resident (or all residents)
        /// for a specified date time range.
        /// See GetCallsForResidentParameters and Call class for details.
        /// 
        /// v2: returns new Call_v2 type
        /// </summary>
        /// <param name="parms">GetCallsForResidentParameters</param>
        /// <remarks name="cat">Call System</remarks>
        /// <returns>L*Call_v2</returns>
        public List<Call_v2> GetCallsForResident_v2(GetCallsForResidentParameters parms)
        {
            return PostTo<List<Call_v2>>(controller: "wzs", json: new
            {
                method = "GetCallsForResident_v2",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method used to retrieve a list of call objects created/answered by your session's userid or a specific user for
        /// a given timerframe.
        /// See GetCallsForUserParameters and Call class for details.
        /// </summary>
        /// <param name="parms">GetCallsForUserParameters</param>
        /// <remarks name="cat">Call System</remarks>
        /// <returns>L*Call</returns>
        public List<Call_v2> GetCallsForUser(GetCallsForUserParameters parms)
        {
            return PostTo<List<Call_v2>>(controller: "wzs", json: new
            {
                method = "GetCallsForUser",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to retrieve a list of CallReasons. These can be used when registereing a call to register the reason/nature of the call.
        /// </summary>
        /// <param name="parms">GetCallReasonsParameters</param>
        /// <remarks name="cat">Call System</remarks> 
        /// /// <returns>L*CallReason</returns>
        public List<CallReason> GetCallReasons(GetCallReasonsParameters parms)
        {
            return PostTo<List<CallReason>>(controller: "wzs", json: new
            {
                method = "GetCalLReasons",
                @params = parms
            });
        }
        ///  <summary>
        /// Method to edit a call. This can be used to close open calls, create new calls, edit existing calls.
        /// When using this method with an CallID that is not found in our database, a new call will be created. (BeginDate, RoomDesc and CallID parameters are mandatory.)
        /// When using this method with an CallID that can be found in our database, the existing call object will be updated with all non-null parameters set in the request.
        /// See EditCallForResidentParameters for details.
        /// </summary>
        /// <param name="parms">EditCallForResidentParameters</param>
        /// <remarks name="cat">Call System</remarks>
        public void EditCallForResident(EditCallForResidentParameters parms)
        {
            PostTo(controller: "wzs", json: new
            {
                method = "EditCallForResident",
                @params = parms
            });
        }
        #endregion

        #region Users
        /// <summary>
        /// Method used to retrieve the User object of a User with a matching iButton property. (E.g. badge number, iButton number.)
        /// See LoginByExternalID, User, class for more info.
        /// 
        /// Returns a list containing a single user.
        /// </summary>
        /// <param name="parms">GetUserForUserKeyParams</param>
        /// <remarks name="cat">Users</remarks>
        /// <returns>L*User</returns>
        public List<User> GetUserForUserKey(GetUserForUserKeyParams parms)
        {
            return PostTo<List<User>>(controller: "wzs", json: new
            {
                method = "GetUserForUserKey",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Returns a list of user objects. The objects contain the info of a user's account as configured in the CS Software.
        /// Can be filtered by active/inactive/userID
        /// </summary>
        /// <param name="parms">GetUSerListParameters</param>
        /// <remarks name="cat">Users</remarks>
        /// <returns>L*User</returns>
        public List<User> GetUserList(GetUSerListParameters parms)
        {
            return PostTo<List<User>>(controller: "wzs", json: new
            {
                method = "GetUserList",
                @params = parms
            }
            );
        }
        #endregion

        #region Medication
        /// <summary>
        /// Method to list all possible vaccinationtypes. You can use the active vaccinationtypes to register new administered vaccinations for residents.
        /// See GetResidentVaccinations, InsertResidentVaccination methods.
        /// </summary>
        /// <param name="parms">GetVaccinationTypeParams</param>
        /// <remarks name="cat">Medication</remarks>
        /// <returns>L*VaccinationType</returns>
        public List<VaccinationType> GetVaccinationTypes(GetVaccinationTypeParams parms)
        {
            return PostTo<List<VaccinationType>>(controller: "wzs", json: new
            {
                method = "GetVaccinationTypes",
                @params = parms
            });
        }
        /// <summary>
        /// Use this method to get the list of planned medication for a date and medication moment. Can be retrieved for a single residents or multiple residents.
        /// This list provides the different medication items that need to be signed as distributed and administered for a resident.
        /// (See SignMedicationPlanItem class and its actiontype property)
        /// </summary>
        /// <param name="parms">GetMedicationPlanForResidentParams</param>
        /// <remarks name="cat">Medication</remarks>
        /// <returns>L*MedicationPlanItem</returns>
        public List<MedicationPlanItem> GetMedicationPlanForResident(GetMedicationPlanForResidentParams parms)
        {
            return PostTo<List<MedicationPlanItem>>(controller: "wzs", json: new
            {
                method = "GetMedicationPlanForResident",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to retrieve the medicationschema for a resident. This is an overview of all active medicine and the distribution schedule and/or remarks.
        /// This is used to generate the MedicationPlan returned by GetMedicationPlanForResident.
        /// This list is built as an overview with parent/child records. E.g. when certain medication is on a special
        /// schedule (alternating time slots,...) the planning details and remarks for this medicine can be retrieved using it's
        /// child records. (ParentID and ID property)
        /// </summary>
        /// <param name="parms">GetMedicationSchemeForResidentParams</param>
        /// <remarks name="cat">Medication</remarks>
        /// <returns>L*MedicationSchemeItem</returns>        
        public List<MedicationSchemeItem> GetMedicationSchemeForResident(GetMedicationSchemeForResidentParams parms)
        {
            return PostTo<List<MedicationSchemeItem>>(controller: "wzs", json: new
            {
                method = "GetMedicationSchemeForResident",
                @params = parms
            });
        }
        /// <summary>
        /// Used to retrieve the different forms in which medication exists. (tablets, cream, injections,...)
        /// See MedicationSchemeItem, MedicationPlanItem, MedicationSchemeItem class.
        /// </summary>
        /// <param name="parms">GetMedicationAdminFormsParams</param>
        /// <remarks name="cat">Medication</remarks>
        /// <returns>L*MedicationAdminForm</returns>
        public List<MedicationAdminForm> GetMedicationAdminForms(GetMedicationAdminFormsParams parms)
        {
            return PostTo<List<MedicationAdminForm>>(controller: "wzs", json: new
            {
                method = "GetMedicationAdminForms",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to retrieve the different medication moments as configured in the care home.
        /// These moments are used when creating a medication scheme/schedule and impact the planning.
        /// See MedicationMoment class.
        /// </summary>
        /// <param name="parms">GetMedicationMomentsParams</param>
        /// <remarks name="cat">Medication</remarks>
        /// <returns>L*MedicationMoment</returns>
        public List<MedicationMoment> GetMedicationMoments(GetMedicationMomentsParams parms)
        {
            return PostTo<List<MedicationMoment>>(controller: "wzs", json: new
            {
                method = "GetMedicationMoments",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to retrieve a list of departments this session has access to.
        /// (If applicable) This is only useful if the CS Software has the option of 'department medication security' enabled.
        /// Currently not actively used in WZSN.Net
        /// </summary>
        /// <param name="parms">GetMedicationDeptAccessParams</param>
        /// <remarks name="cat">Medication</remarks>
        /// <returns>L*MedicationDeptAccess</returns>
        public List<MedicationDeptAccess> GetMedicationDeptAccess(GetMedicationDeptAccessParams parms)
        {
            return PostTo<List<MedicationDeptAccess>>(controller: "wzs", json: new
            {
                method = "GetMedicationDeptAccess",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to sign a planned Medication Item distributed or administered. 
        /// Distribute: action of handing out medication to resident
        /// Administered: resident has taken the medication.
        /// 
        /// (Currently WZS.Net chooses to support Actiontype 3: distribute medication // 4: administer medication. 
        /// Actionypes 1: Prepare medication // 2: review medication are registered in the CS Software itself.)
        /// 
        /// This method is used to sign the MedicationplanItems returned by the GetMedicationPlanForReisdent objects.
        /// 
        /// See MedicationMoment, MedicationPlanItem classes for details.
        /// 
        /// Returns the ID of the created signature record.
        /// </summary>
        /// <param name="parms">SignMedicationPlanItemParams</param>
        /// <remarks name="cat">Medication</remarks>
        /// <returns>IDValue</returns>
        public IDValue SignMedicationPlanItem(SignMedicationPlanItemParams parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "SignMedicationPlanItem",
                @params = parms
            });
        }
        /// <summary>
        /// This method can be used to sign a planned Resident's MedicationPlanItem as not done.
        /// This will sign the item as not done in the database, however, when performing a new GetMedicationPlanForResident this
        /// item will still be marked as to be done.
        /// 
        /// Returns the ID of the created signature record.
        /// </summary>
        /// <param name="parms">SignMedicationPlanItemNotDoneParams</param>
        /// <remarks name="cat">Medication</remarks>
        /// <returns>IDValue</returns>
        public IDValue SignMedicationPlanItemNotDone(SignMedicationPlanItemNotDoneParams parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "SignMedicationPlanItemNotDone",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to sign a  standing order as given. (E.g. when resident demands a painkiller)
        /// Standing orders are not distributed on a planning, but instead can be administered when a resident has need for it. (Painkillers, creams...)
        ///
        /// Returns the ID value of the created signature.
        /// </summary>
        /// <param name="parms">SignMedicationStandingOrderParams</param>
        /// <remarks name="cat">Medication</remarks>
        /// <returns>IDValue</returns>
        public IDValue SignMedicationStandingOrder(SignMedicationStandingOrderParams parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "SignMedicationStandingOrder",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to validate a resident's medication scheme. After changes are made (if configured in the application) to a resident's medication
        /// scheme. These changes need to be validated using this method by the resident's doctor. Only doctor user accounts van validate these schemes.
        /// 
        /// Returns the count of medicationscheme lines that were validated.
        /// </summary>
        /// <param name="parms">ValidateMedicationSchemeForResidentParams</param>
        /// <remarks name="cat">Medication</remarks>
        /// <returns>CountValue</returns>
        public CountValue ValidateMedicationSchemeForResident(ValidateMedicationSchemeForResidentParams parms)
        {
            return PostTo<CountValue>(controller: "wzs", json: new
            {
                method = "ValidateMedicationSchemeForResident",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to retrieve a list of all signed standing orders for a list of residents.
        /// Can be filtered by department or resident.
        /// </summary>
        /// <param name="parms">GetMedicationStandingOrderLogParams</param>
        /// <remarks name="cat">Medication</remarks>
        /// <returns>L*MedicationStandingOrderLogItem</returns>
        public List<MedicationStandingOrderLogItem> GetMedicationStandingOrdersLog(GetMedicationStandingOrderLogParams parms)
        {
            return PostTo<List<MedicationStandingOrderLogItem>>(controller: "wzs", json: new
            {
                method = "GetMedicationStandingOrdersLog",
                @params = parms
            });
        }
        #endregion

        #region Wounds
        /// <summary>
        /// Method to retrieve the different WoundCategories used in the CS Application.
        /// Each wound needs to be classified as one of these categories. (E.g. Burn wounds, cuts, scrape wounds,...)
        /// 
        /// Can be filtered to retrieve only active, inactive or both active and inactive wound categories.
        /// </summary>
        /// <param name="parms">GetWoundCategoriesParams</param>
        /// <remarks name="cat">Wounds</remarks>
        /// <returns>L*WoundCategory</returns>
        public List<WoundCategory> GetWoundCategories(GetWoundCategoriesParams parms)
        {
            return PostTo<List<WoundCategory>>(controller: "wzs", json: new
            {
                method = "GetWoundCategories",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to retrieve a list of the different WoundOrigins known in the application.
        /// These are used when registering a new wound. A woundorigin defines where the user was injured. (Hospital, Care Home,...)
        /// </summary>
        /// <param name="parms">GetWoundOriginsParams</param>
        /// <remarks name="cat">Wounds</remarks>
        /// <returns>L*WoundOrigin</returns>
        public List<WoundOrigin> GetWoundOrigins(GetWoundOriginsParams parms)
        {
            return PostTo<List<WoundOrigin>>(controller: "wzs", json: new
            {
                method = "GetWoundOrigins",
                @params = parms
            });
        }
        /// <summary>
        /// Method to retrieve info of a set of WoundClassificationFields.
        /// e.g. retrieves the possible values of a classification's field multiselect list. retrieves the data type of the field,....
        /// 
        /// See WoundClassification class for details.
        /// </summary>
        /// <param name="parms">GetWoundClassificationFieldsParams</param>
        /// <remarks name="cat">Wounds</remarks>
        /// <returns>L*WoundClassificationField</returns>
        public List<WoundClassificationField> GetWoundClassificationFields(GetWoundClassificationFieldsParams parms)
        {
            return PostTo<List<WoundClassificationField>>(controller: "wzs", json: new
            {
                method = "GetWoundClassificationFields",
                @params = parms
            });
        }
        /// <summary>
        /// Method can be used to retrieve a list of wounds (active/inactive) for a single resident or a set of residents.
        /// The wounds will be returned including their classification(s) in the Classifications property.
        /// 
        /// This method can be filtered.
        /// </summary>
        /// <param name="parms">GetWoundsParams</param>
        /// <remarks name="cat">Wounds</remarks>
        /// <returns>L*ResidentWound</returns>
        public List<ResidentWound> GetWounds(GetWoundsParams parms)
        {
            return PostTo<List<ResidentWound>>(controller: "wzs", json: new
            {
                method = "GetWounds",
                @params = parms
            });
        }
        /// <summary>
        /// Method to insert a new wound for a resident.
        /// This method requires a first classification to be passed on in the Classification parameter property.
        /// See the classes in the Wound region (return types) for more info on how to build a valid woundclassification.
        /// 
        /// Returns the ID value of the new wound record.
        /// </summary>
        /// <param name="parms">InsertResidentWoundParameters</param>
        /// <remarks name="cat">Wounds</remarks>
        /// <returns>IDValue</returns>
        public IDValue InsertResidentWound(InsertResidentWoundParameters parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "InsertResidentWound",
                @params = parms
            });
        }
        /// <summary>
        /// This method is used to re-evaluate or re-classify an existing wound.
        /// The different classifications describe a wound's healing process over time. Each classification is a snapshot of the wound status at that moment.
        /// Changing a wound's classification can have impact on the wound's treatment protocol. A wound can be even be reclassified as a different category. 
        /// 
        /// Returns the ID value of the new Classification record.
        /// </summary>
        /// <param name="parms">InsertResidentWoundClassificationParameters</param>
        /// <remarks name="cat">Wounds</remarks>
        /// <returns>IDValue</returns>
        public IDValue InsertResidentWoundClassification(InsertResidentWoundClassificationParameters parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "InsertResidentWoundClassification",
                @params = parms
            });
        }
        #endregion

        #region Invoicing
        /// <summary>
        /// Method used to book a cost/good/service to be invoiced for a resident.
        /// At the moment the customer needs to have bought the Purchasing module to make full use of this method.
        /// It enables third party applications to directly book costs/services into the CS Software's invoicing module.
        /// This method essentially enables the third party application to create invoice lines on a resident’s invoice with invoice codes as configured 
        /// in the CS Software.
        /// 
        /// A vendor number for you application needs to have been created by the customer.
        /// 
        /// See InsertVendorInvoiceLineParams class for details
        /// 
        /// Return the new ID value for inserted invoice line.
        /// </summary>
        /// <param name="parms">InsertVendorInvoiceLineParams</param>
        /// <remarks name="cat">Invoicing</remarks>
        /// <returns>IDValue</returns>
        public IDValue InsertVendorInvoiceLine(InsertVendorInvoiceLineParams parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "InsertVendorInvoiceLine",
                @params = parms
            });
        }
        /// <summary>
        /// Method used to retrieve a list of existing invoice codes as configured in the CS Software.
        /// Can be filtered to retrieve only active, inactive or bot inactive and active invoice codes.
        /// These codes are used in the InserVendorInvoiceLine method.
        /// </summary>
        /// <param name="parms">GetInvoiceCodesParams</param>
        /// <remarks name="cat">Invoicing</remarks>
        /// <returns>L*InvoiceCode</returns>
        public List<InvoiceCode> GetInvoiceCodes(GetInvoiceCodesParams parms)
        {
            return PostTo<List<InvoiceCode>>(controller: "wzs", json: new
            {
                method = "GetInvoiceCodes",
                @params = parms
            });
        }
        #endregion

        #region Redirects
        /// <summary>
        /// Gets a url for an external link for a resident. (eg. direct link to a resident's file in a third party web
        /// application)
        /// </summary>
        /// <remarks name="cat">Redirects</remarks>
        /// <param name="parms">GetRedirectURLForResidentParams</param>
        /// <returns>string</returns>
        public string GetRedirectURLForResident(GetRedirectURLForResidentParams parms)
        {
            return PostTo<string>(controller: "wzs", json: new
            {
                method = "GetRedirectURLForResident",
                @params = parms
            });
        }
        /// <summary>
        /// Will return all resident id's that can be used with a certain redirect type.
        /// Eg. if only a select number of residents are known in a third party application.
        /// </summary>
        /// <param name="parms">GetResidentIDsForRedirectParams</param>
        /// <remarks name="cat">Redirects</remarks>
        /// <returns>L*int</returns>
        public List<IDValue> GetResidentIDsForRedirect(GetResidentIDsForRedirectParams parms)
        {
            return PostTo<List<IDValue>>(controller: "wzs", json: new
            {
                method = "GetResidentIDsForRedirect",
                @params = parms
            });
        }        
        #endregion  

        #region WaitingList       
        /// <summary>
        /// Method used to edit an exisiting or create a new WaitingListEntry. If you wish to edit an existing parameter. Set the WaitingListEntryID to null.
        /// If you want to update/edit an existing entry. Set the WaitinglistEntryID to the ID you wish to edit. Note, always pass a completed parameter object. The method
        ///  will update ALL database fields as passed in the parameters. All Parameters passed as NULL will be set to NULL in the database.
        /// 
        /// If succesful, returns the ID of the edited WaitingListEntry or the id of a newly created WaitingListEntry if ID was passed as NULL.
        /// </summary>
        /// <param name="parms">EditWaitingListEntryParams</param>
        /// <remarks name="cat">WaitingList</remarks>
        /// <returns>IDValue</returns>
        public IDValue EditWaitingListEntry(EditWaitingListEntryParams parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "EditWaitingListEntry",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to retrieve a list of existing WaitingListEntries. You can filter by a list of WaitingListEntryID's and/or status.
        /// </summary>
        /// <param name="parms">GetWaitingListEntriesParams</param>
        /// <remarks name="cat">WaitingList</remarks>
        /// <returns>L*WaitingListEntry</returns>
        public List<WaitingListEntry> GetWaitingListEntries(GetWaitingListEntriesParams parms)
        {
            return PostTo<List<WaitingListEntry>>(controller: "wzs", json: new
            {
                method = "GetWaitingListEntries",
                @params = parms
            }
            );
        }        
         /// <summary>
        /// Method to add or edit an existing WaitingListContact for a WaitingListEntry. Currently 2 contacts are supported. Set ContacNumber to 1 or 2
        /// to edit the correct contact. Returns the ID of the associated WaitingListEntry.
        /// </summary>
        /// <param name="parms">EditWaitingListContactParams</param>
        /// <remarks name="cat">WaitingList</remarks>
        /// <returns>IDValue</returns>
        public IDValue EditWaitingListContact(EditWaitingListContactParams parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "EditWaitingListContact",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to retrieve all known waitinglist statusses. 
        /// </summary>
        /// <param name="parms">GetWaitingListStatusParams</param>
        /// <remarks name="cat">WaitingList</remarks>
        /// <returns>L*WaitingListStatus</returns>
        public List<WaitingListStatus> GetWaitingListStatus (GetWaitingListStatusParams parms)
        {
            return PostTo<List<WaitingListStatus>>(controller: "wzs", json: new
            {
                method = "GetWaitingListStatus",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to remove an existing WaitingListContact for a WaitingListEntry. Currently 2 contacts are supported. Set ContacNumber to 1 or 2
        /// to remove the correct contact. Returns the ID of the associated WaitingListEntry.
        /// </summary>
        /// <param name="parms">RemoveWaitingListContactParams</param>
        /// <remarks name="cat">WaitingList</remarks>
        /// <returns>IDValue</returns>
        public IDValue RemoveWaitingListContact(RemoveWaitingListContactParams parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "RemoveWaitingListContact",
                @params = parms
            }
            );
        }
        
        #endregion WaitingList

        #region FallReg       
        /// <summary>
        /// Method to retrieve Fall Incidents from the database. Can be filtered using GetFallIncidentsParameters.
        /// </summary>
        /// <param name="parms">GetFallIncidentsParameters</param>
        /// <remarks name="cat">FallIncidents</remarks>
        /// <returns>L*FallIncident</returns>
        public List<FallIncident> GetFallIncidents(GetFallIncidentsParameters parms)
        {
            return PostTo<List<FallIncident>>(controller: "wzs", json: new
            {
                method = "GetFallIncidents",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to register a Fall Incident in the database.
        /// </summary>
        /// <param name="parms">RegisterFallIncidentParameters</param>
        /// <remarks name="cat">FallIncidents</remarks>
        /// <returns>IDValue</returns>
        public IDValue RegisterFallIncident(RegisterFallIncidentParameters parms)
        {
            return PostTo<IDValue>(controller: "wzs", json: new
            {
                method = "RegisterFallIncident",
                @params = parms
            }
            );
        }
        #endregion FallReg

        #region AppNotifications
        /// <summary>
        /// Method to retrieve all currently valid serverwide AppNotifications.
        /// </summary>
        /// <param name="parms">GetCurrentAppNotificationsParameters</param>
        /// <remarks name="cat">AppNotifications</remarks>
        /// <returns>L*AppNotification</returns>
        public List<AppNotification> GetCurrentAppNotifications(GetCurrentAppNotificationsParameters parms)
        {
            return PostTo<List<AppNotification>>(controller: "wzs", json: new
            {
                method = "GetCurrentAppNotifications",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to edit an existing or create a new AppNotification.
        /// </summary>
        /// <param name="parms">EditAppNotificationParameters</param>
        /// <remarks name="cat">AppNotifications</remarks>
        /// <returns>Guid</returns>
        public List<AppNotification> EditAppNotification(EditAppNotificationParameters parms)
        {
            return PostTo<List<AppNotification>>(controller: "wzs", json: new
            {
                method = "EditAppNotification",
                @params = parms
            }
            );
        }
        /// <summary>
        /// Method to remove an existing AppNotification
        /// </summary>
        /// <param name="parms">RemoveAppNotificationParameters</param>
        /// <remarks name="cat">AppNotifications</remarks>
        /// <returns>Guid</returns>
        public void RemoveAppNotification(RemoveAppNotificationParameters parms)            
        {
            PostTo(controller: "wzs", json: new
            {
                method = "RemoveAppNotification",
                @params = parms
            }
            );
        }
        #endregion

    }


}

