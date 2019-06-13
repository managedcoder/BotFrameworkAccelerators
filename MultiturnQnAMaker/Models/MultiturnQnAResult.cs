// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace QnA.MultiturnQnAMaker
{
    public class MultiturnQnAResult
    {
        public string[] Questions { get; set; }

        public string Answer { get; set; }

        public double Score { get; set; }

        public int Id { get; set; }

        public string Source { get; set; }

        public MultiturnQnAMetadata[] Metadata { get; }

        public MultiturnQnAContext Context { get; set; }
    }
}
