using System;
using System.Collections.Generic;

namespace _7.Entities.Models;

public partial class VariableTimeDuration : BaseEntityId
{
    //public new long Id { get; set; }

    public int? Time { get; set; }
}

public class VariableSetting : BaseEntityId
{
    public required List<VariableTimeDuration> Duration { get; set; }
    public required List<VariableTimeExtend> TimeExtend { get; set; }
}