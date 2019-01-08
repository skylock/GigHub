﻿using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface INotificationsRepository
    {
        List<Notification> GetNewNotificationsFor(string userId);
    }
}