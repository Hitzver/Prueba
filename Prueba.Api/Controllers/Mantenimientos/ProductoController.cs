using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba.Api.ContextoDatos;
using Prueba.Api.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Prueba.Api.Dapper;

namespace Prueba.Api.Controllers.Mantenimientos
{
    //[Authorize]
    [Route("Productos")]
    public class ProductoController
    {
        #region ContextDB

        //private readonly AppDbContext Db;
        private readonly IDapper _dapper;
        public ProductoController(IDapper dapper)
        {
            _dapper = dapper;
        }

        #endregion


        [HttpGet]
        public async Task<ResultHttp> Get()
        {
            try
            {
                var result = await Task.FromResult(_dapper.GetAll<Producto>($"GetProductos", null, commandType: CommandType.StoredProcedure));

                return new ResultHttp
                {
                    Success = true,
                    Mensaje = "Consulta exitosa",
                    Resultado = result
                };
            }
            catch (Exception err)
            {
                return new ResultHttp
                {
                    Success = false,
                    Mensaje = err.ToString(),
                    Resultado = { }
                };
            }
        }

        [HttpGet("{id}")]
        public async Task<ResultHttp> Get(int id)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("Id", id, DbType.Int32);
                return new ResultHttp
                {
                    Success = true,
                    Mensaje = "Consulta exitosa",
                    Resultado = await Task.FromResult(_dapper.Get<Producto>($"GetProductosById", dbparams, commandType: CommandType.StoredProcedure))
                };
            }
            catch (Exception err)
            {
                return new ResultHttp
                {
                    Success = false,
                    Mensaje = err.ToString(),
                    Resultado = { }
                };
            }
        }

        [HttpPost]
        public async Task<ResultHttp> Post([FromBody] Producto x)
        {
            try
            {
                var dbparams = new DynamicParameters();
                dbparams.Add("Nombre", x.Nombre, DbType.String);
                dbparams.Add("Precio", x.Precio, DbType.Double);
                dbparams.Add("Stock", x.Stock, DbType.Int32);
                dbparams.Add("FechaRegistro", x.FechaRegistro, DbType.DateTime);

                var validator = new ProductoValidator();
                var result = validator.Validate(x);
                if (!result.IsValid)
                {
                    return new ResultHttp
                    {
                        Mensaje = result.Errors.FirstOrDefault().ErrorMessage,
                        Success = false,
                    };
                }
                    

                var product = await Task.FromResult(_dapper.Insert<Producto>($"InsertProduct", dbparams,
                    commandType: CommandType.StoredProcedure));

                return new ResultHttp
                {
                    Mensaje = "Se realizo correctamente lo solicitado",
                    Success = true,
                    Resultado = x
                };
            }
            catch (Exception err)
            {
                return new ResultHttp
                {
                    Mensaje = err.Message,
                    Success = false,
                };
            }
        }

        [HttpPut]
        public async Task<ResultHttp> Put([FromBody] Producto x)
        {
            try
            {

                var dbparams = new DynamicParameters();
                dbparams.Add("Id", x.Id, DbType.String);
                dbparams.Add("Nombre", x.Nombre, DbType.String);
                dbparams.Add("Precio", x.Precio, DbType.Double);
                dbparams.Add("Stock", x.Stock, DbType.Int32);
                dbparams.Add("FechaRegistro", x.FechaRegistro, DbType.DateTime);
                var product = await Task.FromResult(_dapper.Update<Producto>($"UpdateProduct", dbparams, CommandType.StoredProcedure));

                return new ResultHttp
                {
                    Mensaje = "Se realizo correctamente lo solicitado",
                    Success = true,
                    Resultado = x
                };
            }
            catch (Exception err)
            {
                return new ResultHttp
                {
                    Mensaje = err.Message,
                    Success = false,
                };
            }
        }

        [HttpDelete("{id}")]
        public async Task<ResultHttp> Delete(int id)
        {
            try
            {

                var dbparams = new DynamicParameters();
                dbparams.Add("Id", id, DbType.String);
                var product = await Task.FromResult(_dapper.Delete<Producto>($"DeleteProductosById", dbparams, CommandType.StoredProcedure));

                return new ResultHttp
                {
                    Success = true,
                    Mensaje = "Registro eliminado correctamente"
                };
            }
            catch (Exception err)
            {
                return new ResultHttp
                {
                    Success = false,
                    Mensaje = err.Message
                };
            }
        }




    }

}
