using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace Ignyt.Framework {
    public class RestHelper : IDisposable {
        private HttpClient _httpClient = null;

        public string Get(string targetUrl) {
            try {
                _httpClient = new HttpClient() { Timeout = new TimeSpan(0, 0, 30), BaseAddress = new Uri(targetUrl) };

                var response = _httpClient.GetAsync(targetUrl).Result;

                return response.Content.ReadAsStringAsync().Result;
            } catch (Exception) {
                throw;
            }
        }

        public KeyValuePair<HttpStatusCode,string> GetStatus(string targetUrl) {
            try {
                _httpClient = new HttpClient() { Timeout = new TimeSpan(0, 0, 30), BaseAddress = new Uri(targetUrl) };

                var response = _httpClient.GetAsync(targetUrl).Result;

                return new KeyValuePair<HttpStatusCode, string>(response.StatusCode, response.Content.ReadAsStringAsync().Result);
            } catch (Exception) {
                throw;
            }
        }

        #region Dispose
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool Disposing) {
            if (_httpClient != null) {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }

        ~RestHelper() {
            Dispose(false);
        }
        #endregion
    }

    public class RestHelper<T> : IDisposable where T : class {
        private HttpClient _httpClient = null;

        public const string DefaultMediaTypeHeaderValue = "application/json";

        public T Get(string targetUrl) {
            try {
                _httpClient = new HttpClient() { Timeout = new TimeSpan(0, 0, 30), BaseAddress = new Uri(targetUrl) };
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(DefaultMediaTypeHeaderValue));

                var response = _httpClient.GetAsync(targetUrl).Result;

                string content = response.Content.ReadAsStringAsync().Result;

                content = content.Substring(1, content.Length - 2);

                content = content.Replace("\\\"", "\"");

                return new JavaScriptSerializer().Deserialize<T>(content);
            } catch (Exception) {
                throw;
            }
        }

        public List<T> GetList(string targetUrl) {
            try {
                _httpClient = new HttpClient() { Timeout = new TimeSpan(0, 0, 30), BaseAddress = new Uri(targetUrl) };
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(DefaultMediaTypeHeaderValue));

                var response = _httpClient.GetAsync(targetUrl).Result;

                string content = response.Content.ReadAsStringAsync().Result;

                content = content.Substring(1, content.Length - 2);

                content = content.Replace("\\\"", "\"");

                return new JavaScriptSerializer().Deserialize<List<T>>(content);
            } catch (Exception) {
                throw;
            }
        }

        #region Dispose
        public void Dispose() {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool Disposing) {
            if (_httpClient != null) {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }

        ~RestHelper() {
            Dispose(false);
        }
        #endregion
    }
}