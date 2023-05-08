
using Newtonsoft.Json;
using Pluto.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls.WebParts;
using static Pluto.Models.Tempon;

namespace Pluto.Repositories
{
    public class CallApiRepositoryImpl : ICAllApi
    {

      //  string urlBase = "http://localhost:8081";
		string urlBase = "http://158.69.120.240:8081";

		public Reponse   CallBackendGet(string url, string tokenKey)
        {
            Reponse reponse = new Reponse();
            string apiAddress = urlBase +url;

            HttpClient httpClient = new HttpClient();
              httpClient.DefaultRequestHeaders.Accept.Clear();
              httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              if (tokenKey != null)
              {
                  httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenKey);

              };
              var response = httpClient.GetAsync(apiAddress);                 

                 try
                 {

                     if (((int)response.Result.StatusCode) == 200)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
                     {
                        reponse = (new JavaScriptSerializer()).Deserialize<Reponse>(response.Result.Content.ReadAsStringAsync().Result);               
                     }
                     else
                     {

                         reponse.code = ((int)response.Result.StatusCode);
                         reponse.message = "La requette a échoué !" + apiAddress;

                     }

                    }
                    
                    catch (WebException e)
                    {
                        if (e.Status == WebExceptionStatus.ProtocolError)
                        {
                               switch (((HttpWebResponse)e.Response).StatusCode)
                            {
                                case HttpStatusCode.Unauthorized:
                                reponse.code = 401;
                                reponse.message = e.Message; break;

                                case HttpStatusCode.BadRequest:
                                reponse.code = 400;
                                reponse.message = e.Message; break;

                                 case HttpStatusCode.NotFound:
                                reponse.code = 404;
                                reponse.message = e.Message; break;

                                default:
                                reponse.code = 500;
                                reponse.message = e.Message; break;
                            }
                        }
                        else
                        {
                            reponse.code =(int) ((HttpWebResponse)e.Response).StatusCode;
                            reponse.message = e.Message;
                        }
                    }
                 return reponse;

        }

        public Reponse CallBackendGetFile(string url, string tokenKey)
        {
            Reponse reponse = new Reponse();
            string apiAddress = urlBase + url;
            HttpClient httpClient = new HttpClient();           
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/jpeg"));            

            if (tokenKey != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenKey);

            };
            try
            {
                var response =  httpClient.GetAsync(apiAddress);

                if (((int)response.Result.StatusCode) == 200)
                {               

                   
                        byte[] image = response.Result.Content.ReadAsByteArrayAsync().Result;
                        if(image.Length != 0)
                        {
                        //Convert byte arry to base64string   
                        string imreBase64Data = Convert.ToBase64String(image);
                        string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                        //Passing image data in viewbag to view 
                        reponse.code = 200;
                        reponse.message = "La requette a reussi !";
                        reponse.result = imgDataURL;
                        }
                        else
                        {
                        
                        reponse.code = 201;
                        reponse.message = "Aucune photo de profil !";

                       }


                        
                }
                else
                {

                    reponse.code = ((int)response.Result.StatusCode);
                    reponse.message = "La requette a échoué !";

                }

            }

            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    switch (((HttpWebResponse)e.Response).StatusCode)
                    {
                        case HttpStatusCode.Unauthorized:
                            reponse.code = 401;
                            reponse.message = e.Message; break;

                        case HttpStatusCode.BadRequest:
                            reponse.code = 400;
                            reponse.message = e.Message; break;

                        case HttpStatusCode.NotFound:
                            reponse.code = 404;
                            reponse.message = e.Message; break;

                        default:
                            reponse.code = 500;
                            reponse.message = e.Message; break;
                    }
                }
                else
                {
                    reponse.code = (int)((HttpWebResponse)e.Response).StatusCode;
                    reponse.message = e.Message;
                }
            }
            return reponse;

        }

        public Reponse CallBackendPost(string url, object obj,string tokenKey)
        {
            Reponse reponse = new Reponse();
           try
             {
                 HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if(tokenKey != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenKey);

                };
                var response = httpClient.PostAsync(urlBase+url, new StringContent(new JavaScriptSerializer().Serialize(obj), Encoding.UTF8, "application/json"));
                try
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                   if (((int)response.Result.StatusCode) == 200)
                    {

                        reponse = (new JavaScriptSerializer()).Deserialize<Reponse>(response.Result.Content.ReadAsStringAsync().Result);

              
                    }
                    else
                    {
                        reponse.code = ((int)response.Result.StatusCode);
                        reponse.message = " La requette a échoué - - " + result;

                    }
                    }
                      catch 
                      {
                          reponse.code = 500;
                          reponse.message = "problème de serveur " ;

                      } 
                 }
                  catch 
                  {
                      reponse.code = 500;
                      reponse.message = "une erreur interne est survenue coté client ";

                  }  
                return reponse;



        }

        public Reponse CallBackendPostFile(string url, HttpPostedFileBase file, string type, string file_name,string tokenKey)
        {
            string apiAddress = urlBase + url;

            using (var multipartFormContent = new MultipartFormDataContent())
            {
                
                Reponse reponse = new Reponse();
                   try
                {
                    HttpClient httpClient = new HttpClient();
               
                    if(file != null)
                    {


						//Add the file
						var fileStreamContent = new StreamContent(file.InputStream);

						String photo_type = Pluto.Properties.Resources.FILE_PHOTO;
						if (type.Equals(photo_type))
						{
							fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/png");
							multipartFormContent.Add(fileStreamContent, name: "file", fileName: file_name + ".png");

						}
						else
						{
							fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
							multipartFormContent.Add(fileStreamContent, name: "file", fileName: file_name + ".pdf");

						}

						multipartFormContent.Add(new StringContent(type), name: "type");
						if (tokenKey != null)
						{
							httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenKey);

						};

						var response = httpClient.PostAsync(apiAddress, multipartFormContent);
						try
						{
							var result = response.Result.Content.ReadAsStringAsync().Result;

							if (((int)response.Result.StatusCode) == 200)
							{

								reponse = (new JavaScriptSerializer()).Deserialize<Reponse>(response.Result.Content.ReadAsStringAsync().Result);


							}
							else
							{
								reponse.code = ((int)response.Result.StatusCode);
								reponse.message = " La requette a échoué";

							}
						}
						catch
						{
							reponse.code = 500;
							reponse.message = "problème de serveur ";

						}

					}
					else
                    {
						reponse.code = 201;
						reponse.message = "Aucun fichier choisi veuillez choisir un svp !";
					}            



                    }
                     catch 
                     {
                         reponse.code = 500;
                         reponse.message = "une erreur interne est survenue coté client ";

                     }  
                return reponse;


            }

        }
    }
}


