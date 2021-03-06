using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using pg.mtd.typedef;
using pg.util.interfaces;

[assembly: InternalsVisibleTo("pg.mtd.test")]

namespace pg.mtd.builder.attributes
{
    internal class MtdImageTableAttributeBuilder : IBinaryAttributeBuilder<MtdImageTableAttribute>
    {
        public MtdImageTableAttribute Build(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException($"Expected byte array \'{nameof(bytes)}\', got \'null\' instead.");
            }

            MtdImageTableAttribute attribute = new MtdImageTableAttribute();
            MtdImageTableRecordAttributeBuilder attributeBuilder = new MtdImageTableRecordAttributeBuilder();
            int currentByteIndex = 0;
            for (int recordIndex = 0; recordIndex < bytes.Length / new MtdImageTableRecord().Size(); recordIndex++)
            {
                byte[] b = new List<byte>(bytes)
                    .GetRange(currentByteIndex, Convert.ToInt32(new MtdImageTableRecord().Size())).ToArray();
                MtdImageTableRecordAttribute a = attributeBuilder.Build(b);
                attribute.Images.Add(a);
                currentByteIndex += Convert.ToInt32(new MtdImageTableRecord().Size());
            }

            return attribute;
        }
    }
}
