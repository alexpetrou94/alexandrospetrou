﻿using Microsoft.JSInterop;

namespace alexandrospetrou.Services {
    
    public class JSService {
        private readonly IJSRuntime JS;

        public JSService(IJSRuntime js) {
            JS = js;
        }

        public async void AddClassByID(string id, string pClass) {
            await JS.InvokeVoidAsync("addClassByID", id, pClass);
        }

        public async void AddClassByClass(string elementClass, string pClass) {
            await JS.InvokeVoidAsync("addClassByClass", elementClass, pClass);
        }

        public async void ToggleClassByID(string id, string pClass) {
            await JS.InvokeVoidAsync("toggleClassByID", id, pClass);
        }

        public async void ToggleClassByClass(string elementClass, string pClass) {
            await JS.InvokeVoidAsync("toggleClassByClass", elementClass, pClass);
        }

        public async void RemoveClassByID(string id, string pClass) {
            await JS.InvokeVoidAsync("removeClassByID", id, pClass);
        }

        public async void RemoveClassByClass(string elementClass, string pClass) {
            await JS.InvokeVoidAsync("removeClassByClass", elementClass, pClass);
        }

        public async Task<bool> HasClassByID(string id, string pClass) {
            return await JS.InvokeAsync<bool>("hasClassByID", id, pClass);
        }

        public async void SendAlert(string msg) {
            await JS.InvokeVoidAsync("sendAlert", msg);
        }

        public async void OpenUrl(string url, bool newTab = true) {
            await JS.InvokeVoidAsync("openUrl", url, newTab);
        }
    }
}
