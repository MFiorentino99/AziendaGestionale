﻿using FileHelpers;
using Test.Models;
using Test.Models.FileHelpers;
using Test.Abstractions;
using AutoMapper.Internal;
using AutoMapper;
using System.Text;

namespace ApplicationLayer
{
    public class FileHelperConversion : IFileHeplerConversion
    {
        private readonly IMapper _mapperInitializer;
        private List<object> _files;
        private static MultiRecordEngine Engine => new MultiRecordEngine(
            typeof(FHCliente),typeof(FHFattura),typeof(FHinterspace));

        public FileHelperConversion(IMapper mapper)
        {
            _files = new List<object>();
            _mapperInitializer = mapper;
        }
        public string GetStringFromDTO(IEnumerable<DTOCliente_Fatture> resp)
        {
            FHinterspace space = new FHinterspace();
            var records = new List<object>();
            
            foreach (DTOCliente_Fatture dto in resp)
            {
                FHCliente fhCliente = _mapperInitializer.Map<FHCliente>(dto);
                records.Add(fhCliente);

                foreach (var fattura in dto.FatturaList)
                {
                    FHFattura fHFattura = _mapperInitializer.Map<FHFattura>(fattura);
                    records.Add(fHFattura);
                }
                records.Add(space);
            }
           
            
            //_files.AddRange(records);
            
            string str = Engine.WriteString(records);
            return str;
        }

        public void SaveRecordText()
        {
            Engine.WriteFile("file.txt",_files.ToArray());
        }

        /* inserire la logia che legge la stringa, la mappa nei dto e li restituisce */
        public List<DTOFattura> GetDTOFromString(string recordsText, out List<DTOCliente> clienteList)
        {
            string id_cliente = "";
            clienteList = new List<DTOCliente>();
            List<DTOFattura> listFattura = new List<DTOFattura> ();
            var result = Engine.ReadFile(recordsText);

            foreach(var item in result)
            {
                if (item.GetType() == typeof(FHCliente))
                {
                    DTOCliente cliente = _mapperInitializer.Map<DTOCliente>(item);
                    clienteList.Add(cliente);
                    id_cliente = cliente.Id_cliente;
                }
                if (item.GetType() == typeof(FHFattura))
                {
                    DTOFattura fattura = _mapperInitializer.Map<DTOFattura>(item);
                    fattura.Id_cliente = id_cliente;
                    listFattura.Add(fattura);

                }
            }
            return listFattura;
        }
    }
}
