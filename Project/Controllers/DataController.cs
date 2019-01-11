using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Modules;

namespace Test.Controllers
{
    [ApiController]
    public class DataController : ControllerBase
    {
        private DataBase db;
        private Hashtable paramMap;

        [Route("db/SelectList")]
        [HttpGet]
        public ActionResult<Hashtable> SelectList()
        {
            db = new DataBase();
            return db.SelectList();
        }

        [Route("db/Insert")]
        [HttpPost]
        public ActionResult<Hashtable> Insert([FromForm] string title, [FromForm] string contents)
        {
            db = new DataBase();
            paramMap = new Hashtable();
            paramMap.Add("title", title);
            paramMap.Add("contents", contents);
            return db.Insert(paramMap);
        }

        [Route("db/Update")]
        [HttpPost]
        public ActionResult<Hashtable> Update([FromForm] string no, [FromForm] string title, [FromForm] string contents)
        {
            db = new DataBase();
            paramMap = new Hashtable();
            paramMap.Add("no", no);
            paramMap.Add("title", title);
            paramMap.Add("contents", contents);
            return db.Update(paramMap);
        }

        [Route("db/Delete")]
        [HttpPost]
        public ActionResult<Hashtable> Delete([FromForm] string no)
        {
            db = new DataBase();
            paramMap = new Hashtable();
            paramMap.Add("no", no);
            return db.Delete(paramMap);
        }
    }
}
