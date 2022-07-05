using Newtonsoft.Json;
using SaborDeMexico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SaborDeMexico.Services
{
    public class Servicio
    {
        private const string APIUri = "https://app-sabordemexico.com/api";
        HttpClient client;
        public Servicio()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Host = "app-sabordemexico.com";
            client.MaxResponseContentBufferSize = 256000;

        }
        public async Task<List<ModelOrden>> ListaOrdenAsync(ModelUbicacion modelE)
        {
            try
            {
                var json = JsonConvert.SerializeObject(modelE);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                List<ModelOrden> model = new List<ModelOrden>();
                var uri = new Uri($"{APIUri}/Clientes/ListaOrdenes");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<List<ModelOrden>>(contentR);
                    return model.OrderBy(d => d.Fecha).ToList();
                }
                else
                    return model;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }
        }

        public async Task<List<ModelDetalleOrden>> DevGenOrden(int id)
        {
            try
            {
                var uri = new Uri($"{APIUri}/Carritoes/" + id);
                var response = await ProcessGetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    List<ModelDetalleOrden> model = JsonConvert.DeserializeObject<List<ModelDetalleOrden>>(contentR);
                    return model;
                }
                else
                    return null;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }
        }
        public async Task<ModelGOrden> GenOrden(ModelGOrden model)
        {
            try
            {

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var uri = new Uri($"{APIUri}/Carritoes/Orden");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<ModelGOrden>(contentR);
                    return model;
                }
                else
                {
                    return model;
                }
            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }
        }
        public async Task<ModelGOrden> CanOrden(ModelGOrden model)
        {
            try
            {

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var uri = new Uri($"{APIUri}/Carritoes/CancelarOrden");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<ModelGOrden>(contentR);
                    return model;
                }
                else
                {
                    return model;
                }
            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }
        }
        public async Task<bool> AddCarrito(ModelCarrito model)
        {

            try
            {


                var uri = new Uri($"{APIUri}/Carritoes/");

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {

                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<ModelCarrito>(contentR);

                    return true;
                }
                else
                    return false;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                return false;

            }

        }
        public async Task<List<ModelDetalleProductos>> ProductoReturn(string id)
        {
            try
            {
                List<ModelDetalleProductos> model = new List<ModelDetalleProductos>();
                var uri = new Uri($"{APIUri}/Productoes/" + id);
                var response = await ProcessGetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<List<ModelDetalleProductos>>(contentR);
                    foreach(var modelDetalleProductos in model)
                    {
                        try
                        {
                            modelDetalleProductos.Imagen = "https://admin.app-sabordemexico.com/" + modelDetalleProductos.IdProducto + ".png";
                            
                        }
                        catch (Exception)
                        {

                         
                        }
                    }
                    return model;
                }
                else
                    return model;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }
        }
        public async Task<List<ModelCategorias>> ListaCategoriasAsync()
        {
            try
            {
                List<ModelCategorias> model = new List<ModelCategorias>();
                var uri = new Uri($"{APIUri}/Clientes/Cate");
                var response = await ProcessGetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<List<ModelCategorias>>(contentR);
                    return model;
                }
                else
                    return model;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }
        }
        public async Task<List<UbicacionGet>> GetDatosReparto()
        {
            List<UbicacionGet> model3 = new List<UbicacionGet>();
            try
            {

             
             
                var uri = new Uri($"{APIUri}/Repartidors/");
                var response = await ProcessGetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model3 = JsonConvert.DeserializeObject<List<UbicacionGet>>(contentR);
                }
                return model3;
            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }
        }

        public async Task<Ubicacion> GetDatosUbicacion(string Token)
        {
            List<Ubicacion> model3 = new List<Ubicacion>();
            try
            {

                var json = JsonConvert.SerializeObject(new ModelUbicacion() { Token = Token });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var uri = new Uri($"{APIUri}/Clientes/ListaUbicacion");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model3 = JsonConvert.DeserializeObject<List<Ubicacion>>(contentR);
                }
                return model3[model3.Count - 1];
            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }
        }     

        public async void SetDatosTokenNotifi(ModelUsuario model)
        {
            try
            {

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var uri = new Uri($"{APIUri}/Clientes/SaveNotifi");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    //model3 = JsonConvert.DeserializeObject<List<Ubicacion>>(contentR);
                }
               
            }
            catch (Exception e)
            {
                var sm = e.Message;
               

            }
        }
        public async Task<List<ModelCarrito>> ListaCarritoAsync(ModelCarrito modelCarrito)
        {
            try
            {
                var json = JsonConvert.SerializeObject(modelCarrito);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                List<ModelCarrito> model = new List<ModelCarrito>();
                var uri = new Uri($"{APIUri}/Carritoes/Lista");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<List<ModelCarrito>>(contentR);
                    return model;
                }
                else
                    return null;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }
        }
        public async Task<bool> DeleteProducCarrito(ModelCarrito model)
        {

            try
            {


                var uri = new Uri($"{APIUri}/Carritoes/BorrarProduc");

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {

                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<ModelCarrito>(contentR);
                    if (model.Nota.Equals("C"))
                    {
                        return true;
                    }
                    return false;
                }
                else
                    return false;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                return false;

            }

        }
        public async Task<List<ModelProducto>> ListaProductosAsync(int id)
        {
            try
            {
                List<ModelProducto> model = new List<ModelProducto>();
                var uri = new Uri($"{APIUri}/Clientes/Produc/{id}");
                var response = await ProcessGetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<List<ModelProducto>>(contentR);
                    if (model.Count!=0)
                    {
                        List <ModelProducto> products = new List<ModelProducto>();
                        foreach (ModelProducto item in model)
                        {
                            try
                            {
                                item.Imagen = "https://admin.app-sabordemexico.com/" + item.Id + ".png";
                                if (item.Descripcion.Length >= 30)
                                {
                                    item.DescriCorta = item.Descripcion.Substring(0, 30);
                                    item.DescriCorta += "...";
                                }
                                else
                                {
                                    item.DescriCorta = item.Descripcion;
                                }
                            }
                            catch (Exception)
                            {

                                item.DescriCorta = "";
                            }
                            products.Add(item);
                        }
                        return products;
                    }
                    return model;
                }
                else
                    return model;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }
        }
        public async Task<ModelUsuario> LoginCAsync(ModelUsuario model)
        {
            try
            {

                var uri = new Uri($"{APIUri}/Clientes/Login");

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<ModelUsuario>(contentR);
                    if (model.Id > 0)
                    {
                        await GuaDatosLog(model);
                    }

                    return model;
                }
                else
                    return model;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                model.Nombre = "Error de nube";
                return model;

            }
        }
        public async Task<ModelUsuario> Registrarme(ModelUsuario model)
        {

            try
            {


                var uri = new Uri($"{APIUri}/Clientes/Registro");

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {

                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<ModelUsuario>(contentR);

                    return model;
                }
                else
                    return null;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }

        }
        public async Task<bool> GetDatosUsuario(string Token)
        {

            try
            {

                var json = JsonConvert.SerializeObject(new ModelUsuario() { Token = Token });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var uri = new Uri($"{APIUri}/Clientes/VerificaC");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    var model3 = JsonConvert.DeserializeObject<bool>(contentR);

                        return model3;
                   
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                var sm = e.Message;
                return false;

            }
        }
        public async Task<ModelYo> GetInformacionUsuario(string Token)
        {

            try
            {

                var json = JsonConvert.SerializeObject(new ModelUsuario() { Token = Token });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var uri = new Uri($"{APIUri}/Clientes/Datos");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    var model3 = JsonConvert.DeserializeObject<ModelYo>(contentR);

                        return model3;
                   
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                var sm = e.Message;
                return null;

            }
        }
        public async Task<ModelUbicacion> AltaUbicaAsync(ModelUbicacion model)
        {
            try
            {

                var uri = new Uri($"{APIUri}/Clientes/Ubicacion");

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<ModelUbicacion>(contentR);
                    return model;
                }
                else
                    return model;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                model.Name = "Error de nube";
                return model;

            }
        }
        public async Task<ModelUsuario> GenerarCAsync(ModelUsuario model)
        {
            try
            {

                var uri = new Uri($"{APIUri}/Clientes/GCodigo");

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<ModelUsuario>(contentR);
                    return model;
                }
                else
                    return model;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                model.Nombre = "Error de nube";
                model.Id = -1;
                return model;

            }
        }
        public async Task<ModelUsuario> VerificacionCAsync(ModelUsuario model)
        {
            try
            {

                var uri = new Uri($"{APIUri}/Clientes/CVerifi");

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<ModelUsuario>(contentR);
                    return model;
                }
                else
                    return model;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                model.Nombre = "Error de nube";
                model.Id = -1;
                return model;

            }
        }
        public async Task<ModelUsuario> CambioContraseCAsync(ModelUsuario model)
        {
            try
            {

                var uri = new Uri($"{APIUri}/Clientes/CContra");

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<ModelUsuario>(contentR);
                    return model;
                }
                else
                    return model;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                model.Nombre = "Error de nube";
                model.Id = -1;
                return model;

            }
        }    
        public async Task<ModelUsuario> ReenviarCAsync(ModelUsuario model)
        {
            try
            {

                var uri = new Uri($"{APIUri}/Clientes/Reenviar");

                var json = JsonConvert.SerializeObject(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await ProcessPostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var contentR = await response.Content.ReadAsStringAsync();
                    model = JsonConvert.DeserializeObject<ModelUsuario>(contentR);
                    return model;
                }
                else
                    return model;


            }
            catch (Exception e)
            {
                var sm = e.Message;
                model.Nombre = "Error de nube";
                model.Id = -1;
                return model;

            }
        }
        public static void CleanDatosToken()
        {
            SecureStorage.RemoveAll();
        }
        private async Task<bool> GuaDatosLog(ModelUsuario classLog)
        {

            try
            {
                await SecureStorage.SetAsync("oauth_token", classLog.Token);
            }
            catch (Exception)
            {

                return false;
            }

            try
            {
                await SecureStorage.SetAsync("Name", classLog.Nombre);
            }
            catch (Exception)
            {

                return false;
            }
            try
            {
                await SecureStorage.SetAsync("EstaUbi", classLog.ResgisUbi.ToString());
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

        private async Task<HttpResponseMessage> ProcessPostAsync(Uri uri, StringContent content)
        {
            return await client.PostAsync(uri, content); ;
        }
        private async Task<HttpResponseMessage> ProcessGetAsync(Uri uri)
        {
            return await client.GetAsync(uri); ;
        }
    }
}
