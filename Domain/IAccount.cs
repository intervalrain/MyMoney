﻿using System;
namespace Domain
{
	public interface IAccount
	{
		public string AccountName { get; }
		public int InitMoney { get; }
        public IReadOnlyList<Item> Items { get; }

		public int Sum();
    }
}

