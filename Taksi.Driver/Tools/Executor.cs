using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Taksi.Driver.Ui;
using Taksi.DTO.DTOs;

namespace Taksi.Driver.Tools
{
    public class Executor
    {
        private readonly Inputter _inputter;
        private readonly Asker _asker;
        private readonly Actions _actions;

        public Executor()
        {
            _inputter = new Inputter();
            _actions = new Actions();
            _asker = new Asker();
        }



        public void RegisterDriver(HttpClient client)
        {
            _actions.RegisterDriver(client);
        }

        
        // public void Order(HttpClient client)
        // {
        //     var ans = _asker.AskChoices("Accept order?",new List<string>() {"Yes", "No"});
        //     if (ans.Equals("Yes"))
        //         _actions.Order();
        // }
    }
}