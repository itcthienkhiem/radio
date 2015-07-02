#region License

// Copyright (c) 2010, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.42.
// 
namespace ClearCanvas.Dicom.Audit {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class AuditMessage {
        
        private EventIdentificationType eventIdentificationField;
        
        private AuditMessageActiveParticipant[] activeParticipantField;
        
        private AuditSourceIdentificationType[] auditSourceIdentificationField;
        
        private ParticipantObjectIdentificationType[] participantObjectIdentificationField;
        
        /// <remarks/>
        public EventIdentificationType EventIdentification {
            get {
                return this.eventIdentificationField;
            }
            set {
                this.eventIdentificationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ActiveParticipant")]
        public AuditMessageActiveParticipant[] ActiveParticipant {
            get {
                return this.activeParticipantField;
            }
            set {
                this.activeParticipantField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AuditSourceIdentification")]
        public AuditSourceIdentificationType[] AuditSourceIdentification {
            get {
                return this.auditSourceIdentificationField;
            }
            set {
                this.auditSourceIdentificationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ParticipantObjectIdentification")]
        public ParticipantObjectIdentificationType[] ParticipantObjectIdentification {
            get {
                return this.participantObjectIdentificationField;
            }
            set {
                this.participantObjectIdentificationField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EventIdentificationType {
        
        private CodedValueType eventIDField;
        
        private CodedValueType[] eventTypeCodeField;
        
        private EventIdentificationTypeEventActionCode eventActionCodeField;
        
        private bool eventActionCodeFieldSpecified;
        
        private System.DateTime eventDateTimeField;
        
        private EventIdentificationTypeEventOutcomeIndicator eventOutcomeIndicatorField;
        
        /// <remarks/>
        public CodedValueType EventID {
            get {
                return this.eventIDField;
            }
            set {
                this.eventIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("EventTypeCode")]
        public CodedValueType[] EventTypeCode {
            get {
                return this.eventTypeCodeField;
            }
            set {
                this.eventTypeCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public EventIdentificationTypeEventActionCode EventActionCode {
            get {
                return this.eventActionCodeField;
            }
            set {
                this.eventActionCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EventActionCodeSpecified {
            get {
                return this.eventActionCodeFieldSpecified;
            }
            set {
                this.eventActionCodeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime EventDateTime {
            get {
                return this.eventDateTimeField;
            }
            set {
                this.eventDateTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public EventIdentificationTypeEventOutcomeIndicator EventOutcomeIndicator {
            get {
                return this.eventOutcomeIndicatorField;
            }
            set {
                this.eventOutcomeIndicatorField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CodedValueType {
        
        private string codeField;
        
        private string codeSystemField;
        
        private string codeSystemNameField;
        
        private string displayNameField;
        
        private string originalTextField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string code {
            get {
                return this.codeField;
            }
            set {
                this.codeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codeSystem {
            get {
                return this.codeSystemField;
            }
            set {
                this.codeSystemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codeSystemName {
            get {
                return this.codeSystemNameField;
            }
            set {
                this.codeSystemNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string displayName {
            get {
                return this.displayNameField;
            }
            set {
                this.displayNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string originalText {
            get {
                return this.originalTextField;
            }
            set {
                this.originalTextField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ParticipantObjectDescriptionType {
        
        private ParticipantObjectDescriptionTypeMPPS[] mPPSField;
        
        private ParticipantObjectDescriptionTypeAccession[] accessionField;
        
        private ParticipantObjectDescriptionTypeSOPClass[] sOPClassField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("MPPS")]
        public ParticipantObjectDescriptionTypeMPPS[] MPPS {
            get {
                return this.mPPSField;
            }
            set {
                this.mPPSField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Accession")]
        public ParticipantObjectDescriptionTypeAccession[] Accession {
            get {
                return this.accessionField;
            }
            set {
                this.accessionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("SOPClass")]
        public ParticipantObjectDescriptionTypeSOPClass[] SOPClass {
            get {
                return this.sOPClassField;
            }
            set {
                this.sOPClassField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ParticipantObjectDescriptionTypeMPPS {
        
        private string uIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UID {
            get {
                return this.uIDField;
            }
            set {
                this.uIDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ParticipantObjectDescriptionTypeAccession {
        
        private string numberField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Number {
            get {
                return this.numberField;
            }
            set {
                this.numberField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ParticipantObjectDescriptionTypeSOPClass {
        
        private ParticipantObjectDescriptionTypeSOPClassInstance[] instanceField;
        
        private string uIDField;
        
        private string numberOfInstancesField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Instance")]
        public ParticipantObjectDescriptionTypeSOPClassInstance[] Instance {
            get {
                return this.instanceField;
            }
            set {
                this.instanceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UID {
            get {
                return this.uIDField;
            }
            set {
                this.uIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        public string NumberOfInstances {
            get {
                return this.numberOfInstancesField;
            }
            set {
                this.numberOfInstancesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ParticipantObjectDescriptionTypeSOPClassInstance {
        
        private string uIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UID {
            get {
                return this.uIDField;
            }
            set {
                this.uIDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TypeValuePairType {
        
        private string typeField;
        
        private byte[] valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="base64Binary")]
        public byte[] value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ParticipantObjectIdentificationType {
        
        private ParticipantObjectIdentificationTypeParticipantObjectIDTypeCode participantObjectIDTypeCodeField;
        
        private object itemField;
        
        private TypeValuePairType[] participantObjectDetailField;
        
        private string[] participantObjectDetailStringField;
        
        private ParticipantObjectDescriptionType[] participantObjectDescriptionField;
        
        private string participantObjectIDField;
        
        private byte participantObjectTypeCodeField;
        
        private bool participantObjectTypeCodeFieldSpecified;
        
        private byte participantObjectTypeCodeRoleField;
        
        private bool participantObjectTypeCodeRoleFieldSpecified;
        
        private byte participantObjectDataLifeCycleField;
        
        private bool participantObjectDataLifeCycleFieldSpecified;
        
        private string participantObjectSensitivityField;
        
        /// <remarks/>
        public ParticipantObjectIdentificationTypeParticipantObjectIDTypeCode ParticipantObjectIDTypeCode {
            get {
                return this.participantObjectIDTypeCodeField;
            }
            set {
                this.participantObjectIDTypeCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ParticipantObjectName", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("ParticipantObjectQuery", typeof(byte[]), DataType="base64Binary")]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ParticipantObjectDetail")]
        public TypeValuePairType[] ParticipantObjectDetail {
            get {
                return this.participantObjectDetailField;
            }
            set {
                this.participantObjectDetailField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ParticipantObjectDetailString")]
        public string[] ParticipantObjectDetailString {
            get {
                return this.participantObjectDetailStringField;
            }
            set {
                this.participantObjectDetailStringField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("ParticipantObjectDescription")]
        public ParticipantObjectDescriptionType[] ParticipantObjectDescription {
            get {
                return this.participantObjectDescriptionField;
            }
            set {
                this.participantObjectDescriptionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ParticipantObjectID {
            get {
                return this.participantObjectIDField;
            }
            set {
                this.participantObjectIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ParticipantObjectTypeCode {
            get {
                return this.participantObjectTypeCodeField;
            }
            set {
                this.participantObjectTypeCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ParticipantObjectTypeCodeSpecified {
            get {
                return this.participantObjectTypeCodeFieldSpecified;
            }
            set {
                this.participantObjectTypeCodeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ParticipantObjectTypeCodeRole {
            get {
                return this.participantObjectTypeCodeRoleField;
            }
            set {
                this.participantObjectTypeCodeRoleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ParticipantObjectTypeCodeRoleSpecified {
            get {
                return this.participantObjectTypeCodeRoleFieldSpecified;
            }
            set {
                this.participantObjectTypeCodeRoleFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte ParticipantObjectDataLifeCycle {
            get {
                return this.participantObjectDataLifeCycleField;
            }
            set {
                this.participantObjectDataLifeCycleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ParticipantObjectDataLifeCycleSpecified {
            get {
                return this.participantObjectDataLifeCycleFieldSpecified;
            }
            set {
                this.participantObjectDataLifeCycleFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ParticipantObjectSensitivity {
            get {
                return this.participantObjectSensitivityField;
            }
            set {
                this.participantObjectSensitivityField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class ParticipantObjectIdentificationTypeParticipantObjectIDTypeCode : CodedValueType {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class AuditSourceIdentificationType {
        
        private AuditSourceIdentificationTypeAuditSourceTypeCode[] auditSourceTypeCodeField;
        
        private string auditEnterpriseSiteIDField;
        
        private string auditSourceIDField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AuditSourceTypeCode")]
        public AuditSourceIdentificationTypeAuditSourceTypeCode[] AuditSourceTypeCode {
            get {
                return this.auditSourceTypeCodeField;
            }
            set {
                this.auditSourceTypeCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AuditEnterpriseSiteID {
            get {
                return this.auditEnterpriseSiteIDField;
            }
            set {
                this.auditEnterpriseSiteIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AuditSourceID {
            get {
                return this.auditSourceIDField;
            }
            set {
                this.auditSourceIDField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class AuditSourceIdentificationTypeAuditSourceTypeCode : CodedValueType {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ActiveParticipantType {
        
        private CodedValueType[] roleIDCodeField;
        
        private string userIDField;
        
        private string alternativeUserIDField;
        
        private string userNameField;
        
        private bool userIsRequestorField;
        
        private string networkAccessPointIDField;
        
        private byte networkAccessPointTypeCodeField;
        
        private bool networkAccessPointTypeCodeFieldSpecified;
        
        public ActiveParticipantType() {
            this.userIsRequestorField = true;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("RoleIDCode")]
        public CodedValueType[] RoleIDCode {
            get {
                return this.roleIDCodeField;
            }
            set {
                this.roleIDCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UserID {
            get {
                return this.userIDField;
            }
            set {
                this.userIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AlternativeUserID {
            get {
                return this.alternativeUserIDField;
            }
            set {
                this.alternativeUserIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool UserIsRequestor {
            get {
                return this.userIsRequestorField;
            }
            set {
                this.userIsRequestorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string NetworkAccessPointID {
            get {
                return this.networkAccessPointIDField;
            }
            set {
                this.networkAccessPointIDField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public byte NetworkAccessPointTypeCode {
            get {
                return this.networkAccessPointTypeCodeField;
            }
            set {
                this.networkAccessPointTypeCodeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NetworkAccessPointTypeCodeSpecified {
            get {
                return this.networkAccessPointTypeCodeFieldSpecified;
            }
            set {
                this.networkAccessPointTypeCodeFieldSpecified = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum EventIdentificationTypeEventActionCode {
        
        /// <remarks/>
        C,
        
        /// <remarks/>
        R,
        
        /// <remarks/>
        U,
        
        /// <remarks/>
        D,
        
        /// <remarks/>
        E,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum EventIdentificationTypeEventOutcomeIndicator {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("0")]
        Success,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("4")]
        MinorFailureActionRestarted,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("8")]
        SeriousFailureActionTerminated,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("12")]
        MajorFailureActionMadeUnavailable,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class AuditMessageActiveParticipant : ActiveParticipantType {
    }
}
