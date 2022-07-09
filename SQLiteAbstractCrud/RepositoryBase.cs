﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace SQLiteAbstractCrud
{
    [ExcludeFromCodeCoverage]
    public abstract class RepositoryBase<T> : IRepository<T>
    {
        private readonly string _table = typeof(T).Name;
        private readonly string _dataSource;
        private static Fields _fields;

        protected RepositoryBase(string pathDbFile)
        {
            CreateDbFileIfDontExists(pathDbFile);
            _dataSource = $"Data Source={pathDbFile}";
            SetFields();

            if (_fields != null)
            {
                CreateTableIfDontExists(_dataSource);
            }
        }

        protected RepositoryBase()
        {
        }

        public virtual T Insert(T t)
        {
            long lastInsertedId = 0;
            using (SQLiteConnection con = new(_dataSource))
            { 
                con.Open();

                var queryValuesAdjust = GetValuesCommas(t, _fields);
                var queryInsert = GetQueryInsert(queryValuesAdjust);

                using (var cmd = new SQLiteCommand(queryInsert, con))
                {
                    cmd.ExecuteNonQuery();
                }

                lastInsertedId = GetLastInsertedId(con);
            }

            var tInserted = Get(lastInsertedId);
            return tInserted;
        }

        private long GetLastInsertedId(SQLiteConnection con)
        {
            using (var cmd = new SQLiteCommand("SELECT last_insert_rowid()", con))
            {
                var foo = cmd.ExecuteScalar();
                return (long)foo;
            }
        }

        public virtual T Update(T t, string field, object value)
        {
            using (SQLiteConnection con = new(_dataSource))
            {
                con.Open();

                var query = GetQueryUpdate(t, field, value);

                using (var cmd = new SQLiteCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            return t;
        }

        public virtual void InsertBatch(List<T> list)
        {
            if (!list.Any())
                return;

            using (SQLiteConnection con = new(_dataSource))
            {
                con.Open();

                var query = GetQueryInsertBatch(list);
                using (var cmd = new SQLiteCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            List<T> entities = new();

            using (SQLiteConnection con = new(_dataSource))
            {
                con.Open();

                var query = GetQueryGetAll();

                using (var cmd = new SQLiteCommand(query, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr == null)
                        {
                            Debug.WriteLine("\r\n****\r\n rdr null");
                            Debug.WriteLine(query);
                        }

                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                var entity = Map(rdr);

                                entities.Add(entity);
                            }
                        }
                    }
                }
            }

            return entities;
        }

        public virtual T Get(object id)
        {
            T entity = default;

            using (SQLiteConnection con = new(_dataSource))
            {
                con.Open();

                var fieldsNames = _fields.Items.Select(x => x.Name).ToList();
                var query = GetQueryGet(fieldsNames, id);
                using (var cmd = new SQLiteCommand(query, con))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            entity = Map(rdr);
                        }
                    }
                }
            }

            return entity;
        }

        /// <summary>
        /// Get records by date (GTE and LTE)
        /// </summary>
        /// <param name="fieldName">Name of the field</param>
        /// <param name="minInclude">Minimum date</param>
        /// <param name="maxInclude">Maximum date</param>
        /// <returns>Records</returns>
        public virtual List<T> GetByDateRange(string fieldName, DateTime minInclude, DateTime maxInclude)
        {
            T entity = default;
            var entities = new List<T>();

            using (SQLiteConnection con = new(_dataSource))
            {
                con.Open();

                var fieldsNames = _fields.Items.Select(x => x.Name).ToList();
                var query = GetQueryDateRange(fieldsNames, fieldName, minInclude, maxInclude);
                using (var cmd = new SQLiteCommand(query, con))
                {

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            entity = Map(rdr);
                            entities.Add(entity);
                        }
                    }
                }
            }

            return entities;
        }

        public virtual void Delete(object id)
        {
            using (SQLiteConnection con = new(_dataSource))
            {
                con.Open();

                var query = GetQueryDelete(id);

                using (var cmd = new SQLiteCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public virtual void DropTable()
        {
            using (SQLiteConnection con = new(_dataSource))
            {
                con.Open();

                using (var cmd = new SQLiteCommand($"DROP TABLE {_table};", con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        #region Private Methods

        private static string GetValuesCommas(T t, Fields fields)
        {
            var queryValuesAdjust = "";
            var queryValues = "";
            foreach (var field in fields.Items.Where(x => !x.IsAutoincrement))
            {
                var rawValue = t.GetType().GetProperty(field.Name).GetValue(t, null);
                object value = "";
                switch (field.TypeCSharp)
                {
                    case "DateTime":
                        value = Convert.SqliteDate((DateTime)rawValue);
                        break;
                    case "Boolean":
                        value = ((bool)rawValue) ? 1 : 0;
                        break;
                    default:
                        value = rawValue.ToString().Replace('"', '\'').Replace("'", "''");
                        break;
                }
                
                queryValues += $"{field.Quote}{value}{field.Quote},";
            }
            queryValuesAdjust = queryValues.Substring(0, queryValues.Length - 1);

            return queryValuesAdjust;
        }
        
        private string GetQueryInsertBatch(List<T> entities)
        {
            var sb = new StringBuilder();
    
            entities.ForEach(entity =>
            {
                var valuesCommas = GetValuesCommas(entity, _fields);

                sb.Append($"({valuesCommas}), ");
            });
    
            var repositoriosValue = sb.ToString().Substring(0, sb.Length - 2);

            var fieldsCommas = GetFieldsCommasFields(_fields.Items.Where(x => !x.IsAutoincrement).ToList());

            var query = $"INSERT OR REPLACE INTO {_table} ({fieldsCommas}) VALUES {repositoriosValue};";
            
            return query;
        }
        
        private string GetQueryDelete(object value)
        {
            var query = $"DELETE FROM {_table} WHERE {_fields.GetPrimaryKeyName()} = {_fields.GetQuotePrimaryKey()}{GetValue(value)}{_fields.GetQuotePrimaryKey()};";
            return query;
        }


        private static string GetValue(object valor)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("1");
            DateTime? valorDateTime = null;
            try
            {
                valorDateTime = (DateTime)valor;
                Console.WriteLine("2");
            }
            catch (Exception ex)
            {
                Console.WriteLine("3 " + ex.ToString());
            }

            Console.WriteLine("4");
            if (valorDateTime.HasValue)
            {
                Console.WriteLine("5");
                if (DateTime.TryParse(valorDateTime.Value.ToString(), out DateTime dateValue))
                {
                    Console.WriteLine("6");
                    return dateValue.Year + "-" + dateValue.Month.ToString().PadLeft(2, '0') + "-" + dateValue.Day.ToString().PadLeft(2, '0') + " " +
                        dateValue.Hour.ToString().PadLeft(2, '0') + ":" + dateValue.Minute.ToString().PadLeft(2, '0') + ":" + dateValue.Second.ToString().PadLeft(2, '0') + "." + dateValue.Millisecond.ToString().PadLeft(3, '0');
                }
            }
            Console.WriteLine("7");
            return valor?.ToString();
        }

        private string GetQueryInsert(string queryValuesAdjust)
        {
            var query = $"INSERT OR REPLACE INTO {_table} " +
                        $"({GetFieldsCommasFields(_fields.Items.Where(x => !x.IsAutoincrement).ToList())}) " +
                        $"VALUES ({queryValuesAdjust});";
            return query;
        }

        private string GetQueryUpdate(T t, string fieldName, object value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var setSb = new StringBuilder(" SET ");
            var pkName = _fields.GetPrimaryKeyName();
            var propertyInfo = t.GetType().GetProperty(pkName);
            var pkValue = propertyInfo.GetValue(t, null);

            var pkValueAdjust = AdjustPkValueToQuery(pkValue);

            var valueAdjust = "";
            if (value.GetType().Name.ToLower() == "string")
            {
                valueAdjust = $"'{value}'";
            }
            else if (value.GetType().Name.ToLower() == "int32")
            {
                valueAdjust = value.ToString();
            }
            
            if (string.IsNullOrEmpty(valueAdjust))
            {
                _ = bool.TryParse(value.ToString(), out bool adj);
                valueAdjust = adj ? "1" : "0";
            }

            foreach (var campo in _fields.Items.Select(x => x.Name).Where(x => x.Equals(fieldName)))
            {
                setSb.Append($"{campo} = {valueAdjust}, ");
            }
            setSb.Remove(setSb.Length - 2, 2);

            var query = $"UPDATE {_table} " +
                        $"{setSb} " +
                        $"WHERE {pkName} = {_fields.GetQuotePrimaryKey()}{pkValueAdjust}{_fields.GetQuotePrimaryKey()} ;";
            return query;
        }

        private static string AdjustPkValueToQuery(object pkValue)
        {
            string newPkValue;

            if (_fields.GetPrimaryKeyType().ToLower() == "datetime")
            {
                var dateTime = (DateTime)pkValue;
                newPkValue = dateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            else
                newPkValue = pkValue.ToString();

            return newPkValue;
        }

        private static string GetFieldsCommas(List<string> fields)
        {
            var queryFields = "";
            foreach (var field in fields)
            {
                queryFields += $"{field},";
            }

            var queryFieldsAdjust = queryFields.Substring(0, queryFields.Length - 1);

            return queryFieldsAdjust;
        }

        private static string GetFieldsCommasFields(List<Field> fields)
        {
            var queryFields = "";
            foreach (Field field in fields)
            {
                queryFields += $"{field.Name},";
            }

            var queryFieldsAdjust = queryFields.Substring(0, queryFields.Length - 1);

            return queryFieldsAdjust;
        }

        private string GetQueryGetAll()
        {
            var query = $"SELECT {GetFieldsCommas(_fields.Items.Select(x => x.Name).ToList())} FROM {_table};";

            return query;
        }

        private string GetQueryCreate()
        {
            var fieldsQuery = _fields.Items.Aggregate("", (current, property) => current + $"{property.Name} {property.TypeSQLite} NOT NULL,");
            var fieldPk = _fields.Items.Where(x => x.IsPrimaryKey).Select(x => x.Name).ToList();
            var hasFieldAutoincrement = _fields.Items.Any(x => x.IsAutoincrement);

            if (fieldPk == null || !fieldPk.Any())
                throw new ApplicationException("Nao foi encontrada nenhuma Chave Primaria na entidade");

            if (fieldPk.Count > 1 && hasFieldAutoincrement)
                throw new ApplicationException("Nao e possivel criar tabela com campo autoincrement como chave primaria dupla");
            
            var queryCreate = $"CREATE TABLE if not exists {_table} ( {fieldsQuery} PRIMARY KEY({GetFieldsCommas(fieldPk)} {(hasFieldAutoincrement ? "AUTOINCREMENT" : "")}))";
            
            return queryCreate;
        }

        private static void SetFields()
        {
            _fields = new Fields();
            
            foreach (var t in typeof(T).GetProperties().OrderBy(x => x.Name))
            {
                var primaryKeyAttribute = t.GetCustomAttributes(typeof(PrimaryKeyAttribute), true);
                var autoincrementAttribute = t.GetCustomAttributes(typeof(AutoIncrementAttribute), true);

                _fields.AddField(t.Name, t.PropertyType.Name, primaryKeyAttribute.Any(), autoincrementAttribute.Any());
            }
        }
        
        private static void CreateDbFileIfDontExists(string dbFile)
        {
            if (!File.Exists(dbFile))
            {
                var pathSplit = dbFile.Split("/");
                var folderPath = string.Join("/", pathSplit.Take(pathSplit.Length - 1).ToArray());
                
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                SQLiteConnection.CreateFile(dbFile);
            }
        }

        private void CreateTableIfDontExists(string dataSource)
        {
            using (var con = new SQLiteConnection(dataSource))
            {
                con.Open();

                using (var cmd = new SQLiteCommand(GetQueryCreate(), con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetQueryGet(List<string> fieldsNames, object value)
        {
            return $"SELECT {GetFieldsCommas(fieldsNames)} FROM {_table} WHERE {_fields.GetPrimaryKeyName()} = {GetQueryWhere(value)}";
        }

        private string GetQueryDateRange(List<string> fieldsNames, string fieldName, DateTime paramMin, DateTime paramMax)
        {
            var query = $"SELECT {GetFieldsCommas(fieldsNames)} FROM {_table} WHERE DATE({fieldName}) >= DATE('{paramMin:yyyy-MM-dd HH:mm:ss.fff}') AND DATE({fieldName}) <= DATE('{paramMax:yyyy-MM-dd HH:mm:ss.fff}') ";
            
            return query;
        }

        private static string GetQueryWhere(object pkValue)
        {
            var pkValueAdjust = AdjustPkValueToQuery(pkValue);

            var quotePk = _fields.GetQuotePrimaryKey();
            return $"{quotePk}{pkValueAdjust}{quotePk}";
        }

        private static T Map(IDataRecord rdr)
        {
            var fieldsCount = _fields.Items.Count;
            
            var objects = new object[fieldsCount];

            for (int i = 0; i < fieldsCount; i++)
            {
                switch (_fields.Items[i].TypeSQLite)
                {
                    case "TEXT":
                        if (_fields.Items[i].TypeCSharp == "DateTime")
                            objects[i] = rdr.GetDateTime(i);
                        else
                            objects[i] = rdr.GetString(i);
                        break;
                    case "INTEGER":
                        if (_fields.Items[i].TypeCSharp == "Boolean")
                            objects[i] = rdr.GetBoolean(i);
                        else
                            objects[i] = rdr.GetInt32(i);
                        break;
                }
            }

            var entity = (T)Activator.CreateInstance(typeof(T), objects);

            return entity;
        }
        
        #endregion Private
    }
}
