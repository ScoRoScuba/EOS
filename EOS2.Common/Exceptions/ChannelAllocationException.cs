namespace EOS2.Common.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    using EOS2.Model;

    [Serializable]
    public class ChannelAllocationException : Exception
    {
        public ChannelAllocationException()
            : base()
        {
        }

        public ChannelAllocationException(string message, int? channelId, int? equipmentId)
            : base(message)
        {
            ChannelId = channelId;
            EquipmentId = equipmentId;
        }

        public ChannelAllocationException(string message)
            : base(message)
        {
        }

        public ChannelAllocationException(string message, int? channelId, int? equipmentId, Exception exception)
            : base(message, exception)
        {
            ChannelId = channelId;
            EquipmentId = equipmentId;
        }

        public ChannelAllocationException(string message, Exception exception)
            : base(message, exception)
        {
        }

        protected ChannelAllocationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
            this.ChannelId = serializationInfo.GetInt16("ChannelId");
            this.EquipmentId = serializationInfo.GetInt16("EquipmentId");
        }

        public int? ChannelId { get; private set; }

        public int? EquipmentId { get; private set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("ChannelId", this.ChannelId);
            info.AddValue("EquipmentId", this.EquipmentId);

            base.GetObjectData(info, context);
        }
    }
}