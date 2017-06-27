//
// ModelCollection.cs
//
// Author:
//       Jonathan Lima <jonathan@pagar.me>
//
// Copyright (c) 2014 Pagar.me
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

#if HAS_ASYNC
using System.Threading.Tasks;
#endif

namespace PagarMe.Base
{
    public class ModelCollection<TModel> where TModel : Model
    {
        private PagarMeService _service;
        private string _endpoint;
        readonly string _endpointPrefix;

        internal ModelCollection(string endpoint)
            : this(null, endpoint){}

        internal ModelCollection(PagarMeService service, string endpoint, string endpointPrefix = "")
        {
            if (service == null)
                service = PagarMeService.GetDefaultService();

            _service = service;
            _endpoint = endpoint;
            _endpointPrefix = endpointPrefix;
        }
        
        public TModel Find(int id, bool load = true)
        {
            return Find(id.ToString(), load);
        }

        public TModel Find(string id, bool load = true)
        {
            var model = CreateInstance ();

            if (load)
                model.Refresh(id);
            else
                model.SetId(id);

            return model;
        }

        public TModel Find(TModel searchParams)
        {
            return FindAll(searchParams).FirstOrDefault();
        }

        #if HAS_ASYNC
        public Task<TModel> FindAsync(int id, bool load = true)
        {
            return FindAsync(id.ToString(), load);
        }

        public async Task<TModel> FindAsync(string id, bool load = true)
        {
            var model = CreateInstance ();

            if (load)
                await model.RefreshAsync(id);
            else
                model.SetId(id);

            return model;
        }
        #endif

        public async Task<TModel> FindAsync(TModel searchParams)
        {
            return (await FindAllAsync(searchParams)).FirstOrDefault();
        }
            
        public IEnumerable<TModel> FindAll(TModel searchParams)
        {
            return FinishFindQuery(BuildFindQuery(searchParams).Execute());
        }
        
        public TModel FindAllObject(TModel searchParams)
        {
            return FinishFindQueryObject(BuildFindQuery(searchParams).Execute());
        }

        public async Task<IEnumerable<TModel>> FindAllAsync(TModel searchParams)
        {
            return FinishFindQuery(await BuildFindQuery(searchParams).ExecuteAsync());
        }

        public PagarMeRequest BuildFindQuery(TModel searchParameters)
        {
            var request = new PagarMeRequest(_service, "GET", _endpointPrefix + _endpoint);
			var keys = searchParameters.ToDictionary(SerializationType.Plain);
            request.Query = searchParameters.BuildQueryForKeys(null, keys);

            return request;
        }

        public IEnumerable<TModel> FinishFindQuery(PagarMeResponse response)
        {
            return JArray.Parse (response.Body).Select ((x) => {
                var model = CreateInstance ();

                model.LoadFrom ((JObject)x);

                return model;
            });
 
        }

        public TModel FinishFindQueryObject(PagarMeResponse response)
        {
            var model = CreateInstance();

            model.LoadFrom(response.Body);

            return model;
            
        }

        private TModel CreateInstance ()
        {
            if (_endpointPrefix == "") {
                return (TModel)Activator.CreateInstance (typeof (TModel), new object[] { _service });
            } else {
                return (TModel)Activator.CreateInstance (typeof (TModel), new object[] { _service, _endpointPrefix });
            }
        }
    }
}

