﻿using ChocolateTycoon.Helpers;

namespace ChocolateTycoon.Core.Models
{
    public class Message
    {
        public static string ErrorMessage { get; private set; }
        public static string Notification { get; private set; }
        public static string MainStorageInfo { get; private set; }



        public static void SetErrorMessage(MessageEnum? received)
        {
            ErrorMessage = null;
            if (received != null)
                ErrorMessage = PosisionEnumHelper.GetDisplayName(received);
        }

        public static void SetNotification(string message)
        {
            Notification = message;
        }

        public static void SetMainStorageInfo(int? succeded, int? failed)
        {
            if (succeded == null && failed == null)
            {
                MainStorageInfo = "";
                return;
            }                

            MainStorageInfo = $"{succeded} {PosisionEnumHelper.GetDisplayName(MessageEnum.ProducedInfo)}" +
                              $" {failed} { PosisionEnumHelper.GetDisplayName(MessageEnum.CharityInfo)}";
        }
    }
}