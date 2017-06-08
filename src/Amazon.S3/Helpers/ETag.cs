﻿using Amazon.Helpers;

namespace Amazon.S3
{
    public struct ETag
    {
        public ETag(string value)
        {
            Value = value;
        }

        public byte[] AsMD5()
        {
            if (Value == null || Value.Contains("-")) return null;

            // Generally the ETAG is the MD5 of the object -- hexidecimal encoded and wrapped in quootes.
            // If the object was uploaded using multipart upload then this is the MD5 all of the upload-part-md5s.

            // Multipart uploads also contain a dash

            // 1f8ada2ce841b291cfcd6b9b4b645044-2

            return HexString.ToBytes(Value.Trim('"'));
        }

        public string Value { get; }

        public static implicit operator string(ETag tag) => tag.Value;
    }
}
