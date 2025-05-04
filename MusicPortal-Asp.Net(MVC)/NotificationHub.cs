using Microsoft.AspNetCore.SignalR;

namespace MusicPortal_Asp.Net_MVC_
{
    /*
    Ключевой сущностью в SignalR, через которую клиенты обмениваются сообщеними 
    с сервером и между собой, является хаб (hub). 
    Хаб представляет некоторый класс, который унаследован от абстрактного класса 
    Microsoft.AspNetCore.SignalR.Hub.
    */
    public class NotificationHub: Hub
    {
    }
}
