using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArraySpeedCompare.Pages {
    public class IndexModel : PageModel {
        public string Message = "";
        public string ArrayTime = "";
        public string ListTime = "";
        public string AppendString = "a";
        public int RepeatCount = 10;
        public string fromArray = "";
        public string fromList = "";
        public void OnGet() {

        }

        public IActionResult OnPost() {
            fromArray = fromList = "";
            string str = Request.Form["AppendString"];
            string cnt = Request.Form["RepeatCount"];
            int count;
            if (!int.TryParse(cnt, out count)){
                Message = "Error, repeat count could not be parsed. Used 10.";
                count = 10;
            }
            if (count < 1)
            {
                Message = "Repeat count must be greater than 0.";
                count = 1;
            }
            if (count > 10000000)
            {
                Message = "Cannot repeat more than 10 million times.";
                count = 10000000;
            }
            DateTime startArray = DateTime.Now;
            for (int i = 0; i < count; i++) {
                fromArray += str;
            }
            DateTime endArray = DateTime.Now;
            
            DateTime startList = DateTime.Now;
            System.Text.StringBuilder stb = new System.Text.StringBuilder();
            for (int i = 0; i < count; i++) {
                stb.Append(str);
            }
            fromList = stb.ToString();
            DateTime endList = DateTime.Now;

            AppendString = str;
            RepeatCount = count;

            ArrayTime = String.Format("{0:c}"
                , endArray - startArray);
            ListTime = String.Format("{0:c}"
                , endList - startList);

            return Page();
        }

    }
}
