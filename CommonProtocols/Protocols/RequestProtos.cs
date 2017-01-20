// Generated by ProtoGen, Version=2.4.1.555, Culture=neutral, PublicKeyToken=55f7125234beb589.  DO NOT EDIT!
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.ProtocolBuffers;
using pbc = global::Google.ProtocolBuffers.Collections;
using pbd = global::Google.ProtocolBuffers.Descriptors;
using scg = global::System.Collections.Generic;
namespace Com.Virtuos.Rocket.NetworkMessage {
  
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public static partial class RequestProtos {
  
    #region Extension registration
    public static void RegisterAllExtensions(pb::ExtensionRegistry registry) {
      registry.Add(global::Com.Virtuos.Rocket.NetworkMessage.PingRequest.RequestNumber);
    }
    #endregion
    #region Static variables
    internal static pbd::MessageDescriptor internal__static_virtuos_PingRequest__Descriptor;
    internal static pb::FieldAccess.FieldAccessorTable<global::Com.Virtuos.Rocket.NetworkMessage.PingRequest, global::Com.Virtuos.Rocket.NetworkMessage.PingRequest.Builder> internal__static_virtuos_PingRequest__FieldAccessorTable;
    #endregion
    #region Descriptor
    public static pbd::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbd::FileDescriptor descriptor;
    
    static RequestProtos() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChV2aXJ0dW9zL3JlcXVlc3QucHJvdG8SB3ZpcnR1b3MaJGdvb2dsZS9wcm90", 
            "b2J1Zi9jc2hhcnBfb3B0aW9ucy5wcm90bxoUdmlydHVvcy9jb21tb24ucHJv", 
            "dG8iYQoLUGluZ1JlcXVlc3QSEQoJdGltZXN0YW1wGAIgASgDMj8KDnJlcXVl", 
            "c3RfbnVtYmVyEhAudmlydHVvcy5NZXNzYWdlGIkGIAEoCzIULnZpcnR1b3Mu", 
            "UGluZ1JlcXVlc3RCNcI+MgohQ29tLlZpcnR1b3MuUm9ja2V0Lk5ldHdvcmtN", 
          "ZXNzYWdlEg1SZXF1ZXN0UHJvdG9z"));
      pbd::FileDescriptor.InternalDescriptorAssigner assigner = delegate(pbd::FileDescriptor root) {
        descriptor = root;
        internal__static_virtuos_PingRequest__Descriptor = Descriptor.MessageTypes[0];
        internal__static_virtuos_PingRequest__FieldAccessorTable = 
            new pb::FieldAccess.FieldAccessorTable<global::Com.Virtuos.Rocket.NetworkMessage.PingRequest, global::Com.Virtuos.Rocket.NetworkMessage.PingRequest.Builder>(internal__static_virtuos_PingRequest__Descriptor,
                new string[] { "Timestamp", });
        global::Com.Virtuos.Rocket.NetworkMessage.PingRequest.RequestNumber = pb::GeneratedSingleExtension<global::Com.Virtuos.Rocket.NetworkMessage.PingRequest>.CreateInstance(global::Com.Virtuos.Rocket.NetworkMessage.PingRequest.Descriptor.Extensions[0]);
        pb::ExtensionRegistry registry = pb::ExtensionRegistry.CreateInstance();
        RegisterAllExtensions(registry);
        global::Google.ProtocolBuffers.DescriptorProtos.CSharpOptions.RegisterAllExtensions(registry);
        global::Com.Virtuos.Rocket.NetworkMessage.BaseProtos.RegisterAllExtensions(registry);
        return registry;
      };
      pbd::FileDescriptor.InternalBuildGeneratedFileFrom(descriptorData,
          new pbd::FileDescriptor[] {
          global::Google.ProtocolBuffers.DescriptorProtos.CSharpOptions.Descriptor, 
          global::Com.Virtuos.Rocket.NetworkMessage.BaseProtos.Descriptor, 
          }, assigner);
    }
    #endregion
    
  }
  #region Messages
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  public sealed partial class PingRequest : pb::GeneratedMessage<PingRequest, PingRequest.Builder> {
    private PingRequest() { }
    private static readonly PingRequest defaultInstance = new PingRequest().MakeReadOnly();
    private static readonly string[] _pingRequestFieldNames = new string[] { "timestamp" };
    private static readonly uint[] _pingRequestFieldTags = new uint[] { 16 };
    public static PingRequest DefaultInstance {
      get { return defaultInstance; }
    }
    
    public override PingRequest DefaultInstanceForType {
      get { return DefaultInstance; }
    }
    
    protected override PingRequest ThisMessage {
      get { return this; }
    }
    
    public static pbd::MessageDescriptor Descriptor {
      get { return global::Com.Virtuos.Rocket.NetworkMessage.RequestProtos.internal__static_virtuos_PingRequest__Descriptor; }
    }
    
    protected override pb::FieldAccess.FieldAccessorTable<PingRequest, PingRequest.Builder> InternalFieldAccessors {
      get { return global::Com.Virtuos.Rocket.NetworkMessage.RequestProtos.internal__static_virtuos_PingRequest__FieldAccessorTable; }
    }
    
    public const int RequestNumberFieldNumber = 777;
    public static pb::GeneratedExtensionBase<global::Com.Virtuos.Rocket.NetworkMessage.PingRequest> RequestNumber;
    public const int TimestampFieldNumber = 2;
    private bool hasTimestamp;
    private long timestamp_;
    public bool HasTimestamp {
      get { return hasTimestamp; }
    }
    public long Timestamp {
      get { return timestamp_; }
    }
    
    public override bool IsInitialized {
      get {
        return true;
      }
    }
    
    public override void WriteTo(pb::ICodedOutputStream output) {
      CalcSerializedSize();
      string[] field_names = _pingRequestFieldNames;
      if (hasTimestamp) {
        output.WriteInt64(2, field_names[0], Timestamp);
      }
      UnknownFields.WriteTo(output);
    }
    
    private int memoizedSerializedSize = -1;
    public override int SerializedSize {
      get {
        int size = memoizedSerializedSize;
        if (size != -1) return size;
        return CalcSerializedSize();
      }
    }
    
    private int CalcSerializedSize() {
      int size = memoizedSerializedSize;
      if (size != -1) return size;
      
      size = 0;
      if (hasTimestamp) {
        size += pb::CodedOutputStream.ComputeInt64Size(2, Timestamp);
      }
      size += UnknownFields.SerializedSize;
      memoizedSerializedSize = size;
      return size;
    }
    public static PingRequest ParseFrom(pb::ByteString data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static PingRequest ParseFrom(pb::ByteString data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static PingRequest ParseFrom(byte[] data) {
      return ((Builder) CreateBuilder().MergeFrom(data)).BuildParsed();
    }
    public static PingRequest ParseFrom(byte[] data, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(data, extensionRegistry)).BuildParsed();
    }
    public static PingRequest ParseFrom(global::System.IO.Stream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static PingRequest ParseFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    public static PingRequest ParseDelimitedFrom(global::System.IO.Stream input) {
      return CreateBuilder().MergeDelimitedFrom(input).BuildParsed();
    }
    public static PingRequest ParseDelimitedFrom(global::System.IO.Stream input, pb::ExtensionRegistry extensionRegistry) {
      return CreateBuilder().MergeDelimitedFrom(input, extensionRegistry).BuildParsed();
    }
    public static PingRequest ParseFrom(pb::ICodedInputStream input) {
      return ((Builder) CreateBuilder().MergeFrom(input)).BuildParsed();
    }
    public static PingRequest ParseFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
      return ((Builder) CreateBuilder().MergeFrom(input, extensionRegistry)).BuildParsed();
    }
    private PingRequest MakeReadOnly() {
      return this;
    }
    
    public static Builder CreateBuilder() { return new Builder(); }
    public override Builder ToBuilder() { return CreateBuilder(this); }
    public override Builder CreateBuilderForType() { return new Builder(); }
    public static Builder CreateBuilder(PingRequest prototype) {
      return new Builder(prototype);
    }
    
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public sealed partial class Builder : pb::GeneratedBuilder<PingRequest, Builder> {
      protected override Builder ThisBuilder {
        get { return this; }
      }
      public Builder() {
        result = DefaultInstance;
        resultIsReadOnly = true;
      }
      internal Builder(PingRequest cloneFrom) {
        result = cloneFrom;
        resultIsReadOnly = true;
      }
      
      private bool resultIsReadOnly;
      private PingRequest result;
      
      private PingRequest PrepareBuilder() {
        if (resultIsReadOnly) {
          PingRequest original = result;
          result = new PingRequest();
          resultIsReadOnly = false;
          MergeFrom(original);
        }
        return result;
      }
      
      public override bool IsInitialized {
        get { return result.IsInitialized; }
      }
      
      protected override PingRequest MessageBeingBuilt {
        get { return PrepareBuilder(); }
      }
      
      public override Builder Clear() {
        result = DefaultInstance;
        resultIsReadOnly = true;
        return this;
      }
      
      public override Builder Clone() {
        if (resultIsReadOnly) {
          return new Builder(result);
        } else {
          return new Builder().MergeFrom(result);
        }
      }
      
      public override pbd::MessageDescriptor DescriptorForType {
        get { return global::Com.Virtuos.Rocket.NetworkMessage.PingRequest.Descriptor; }
      }
      
      public override PingRequest DefaultInstanceForType {
        get { return global::Com.Virtuos.Rocket.NetworkMessage.PingRequest.DefaultInstance; }
      }
      
      public override PingRequest BuildPartial() {
        if (resultIsReadOnly) {
          return result;
        }
        resultIsReadOnly = true;
        return result.MakeReadOnly();
      }
      
      public override Builder MergeFrom(pb::IMessage other) {
        if (other is PingRequest) {
          return MergeFrom((PingRequest) other);
        } else {
          base.MergeFrom(other);
          return this;
        }
      }
      
      public override Builder MergeFrom(PingRequest other) {
        if (other == global::Com.Virtuos.Rocket.NetworkMessage.PingRequest.DefaultInstance) return this;
        PrepareBuilder();
        if (other.HasTimestamp) {
          Timestamp = other.Timestamp;
        }
        this.MergeUnknownFields(other.UnknownFields);
        return this;
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input) {
        return MergeFrom(input, pb::ExtensionRegistry.Empty);
      }
      
      public override Builder MergeFrom(pb::ICodedInputStream input, pb::ExtensionRegistry extensionRegistry) {
        PrepareBuilder();
        pb::UnknownFieldSet.Builder unknownFields = null;
        uint tag;
        string field_name;
        while (input.ReadTag(out tag, out field_name)) {
          if(tag == 0 && field_name != null) {
            int field_ordinal = global::System.Array.BinarySearch(_pingRequestFieldNames, field_name, global::System.StringComparer.Ordinal);
            if(field_ordinal >= 0)
              tag = _pingRequestFieldTags[field_ordinal];
            else {
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              continue;
            }
          }
          switch (tag) {
            case 0: {
              throw pb::InvalidProtocolBufferException.InvalidTag();
            }
            default: {
              if (pb::WireFormat.IsEndGroupTag(tag)) {
                if (unknownFields != null) {
                  this.UnknownFields = unknownFields.Build();
                }
                return this;
              }
              if (unknownFields == null) {
                unknownFields = pb::UnknownFieldSet.CreateBuilder(this.UnknownFields);
              }
              ParseUnknownField(input, unknownFields, extensionRegistry, tag, field_name);
              break;
            }
            case 16: {
              result.hasTimestamp = input.ReadInt64(ref result.timestamp_);
              break;
            }
          }
        }
        
        if (unknownFields != null) {
          this.UnknownFields = unknownFields.Build();
        }
        return this;
      }
      
      
      public bool HasTimestamp {
        get { return result.hasTimestamp; }
      }
      public long Timestamp {
        get { return result.Timestamp; }
        set { SetTimestamp(value); }
      }
      public Builder SetTimestamp(long value) {
        PrepareBuilder();
        result.hasTimestamp = true;
        result.timestamp_ = value;
        return this;
      }
      public Builder ClearTimestamp() {
        PrepareBuilder();
        result.hasTimestamp = false;
        result.timestamp_ = 0L;
        return this;
      }
    }
    static PingRequest() {
      object.ReferenceEquals(global::Com.Virtuos.Rocket.NetworkMessage.RequestProtos.Descriptor, null);
    }
  }
  
  #endregion
  
}

#endregion Designer generated code