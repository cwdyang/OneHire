﻿using System.ServiceModel;

namespace Datacom.CorporateSys.HireAPI
{
    [ServiceContract]
    public interface ICandidateService
    {

        [OperationContract]
        Candidate GetCandidate(string emailAddress);
    }
}