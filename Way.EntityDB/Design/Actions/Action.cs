﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace Way.EntityDB.Design.Actions
{
    public abstract class Action
    {
        public int ID { get; set; }
        public abstract void Invoke( EntityDB.IDatabaseService invokingDB);
        internal abstract void BeforeSave();
        public object Save( EJ.DB.easyjob db , int databaseid)
        {
            BeforeSave();

            var action = new EntityDB.CustomDataItem("__action" , "id" , null);
            action.SetValue("type", this.GetType().Name);
            action.SetValue("databaseid",databaseid);
            action.SetValue("content", this.ToJsonString());

            db.Insert(action);
            return action.GetValue("id");
        }
    }

}