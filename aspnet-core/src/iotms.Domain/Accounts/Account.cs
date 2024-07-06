using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using iotms.Devices;

using Volo.Abp;

namespace iotms.Accounts
{
    public abstract class AccountBase : FullAuditedAggregateRoot<Guid>
    {
        [CanBeNull]
        public virtual string? Name { get; set; }

        [CanBeNull]
        public virtual string? Location { get; set; }

        [CanBeNull]
        public virtual string? Address { get; set; }

        [CanBeNull]
        public virtual string? Contact { get; set; }

        [CanBeNull]
        public virtual string? Email { get; set; }

        [CanBeNull]
        public virtual string? Web { get; set; }

        public virtual int Rooms { get; set; }

        public virtual bool Status { get; set; }

        public ICollection<Device> Devices { get; private set; }

        protected AccountBase()
        {

        }

        public AccountBase(Guid id, int rooms, bool status, string? name = null, string? location = null, string? address = null, string? contact = null, string? email = null, string? web = null)
        {

            Id = id;
            Check.Length(name, nameof(name), AccountConsts.NameMaxLength, 0);
            Check.Length(location, nameof(location), AccountConsts.LocationMaxLength, 0);
            Check.Length(address, nameof(address), AccountConsts.AddressMaxLength, 0);
            Check.Length(web, nameof(web), AccountConsts.WebMaxLength, 0);
            Rooms = rooms;
            Status = status;
            Name = name;
            Location = location;
            Address = address;
            Contact = contact;
            Email = email;
            Web = web;
            Devices = new Collection<Device>();
        }

    }
}