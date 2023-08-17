using System.Text;
using System.Threading;
using WebSocketSharp.Server;

namespace WS.Demo.Controller {
    public class BroadcastBehavior : WebSocketBehavior {
        /// <summary>
        /// 建構子
        /// </summary>
        public BroadcastBehavior() { }

        /// <summary>
        /// WebSocket會話建立後調用Fn
        /// </summary>
        protected override void OnOpen() {

            /***
             * 一直從MessageQueueSingleton取出字串,正確取得就進行廣播
             ***/
            while (true) {
                string msg = null;
                MessageQueueSingleton.Instance().GetMsg(out msg);
                if (msg != null) {
                    Sessions.BroadcastAsync(Encoding.UTF8.GetBytes(msg), null);
                }
                Thread.Sleep(1);
            }
        }
    }
}
