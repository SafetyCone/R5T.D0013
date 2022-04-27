using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using R5T.T0064;


namespace R5T.D0013
{
    // Async, high memory usage since there is no such thing 
    [ServiceImplementationMarker]
    public abstract class LineBasedTextFileSerializerBase<T> : IFileSerializer<T>, IServiceImplementation
    {
        protected abstract Task<T> BuildFromLines(string[] lines);

        protected abstract Task<List<string>> GetLines(T value);

        public async Task<T> Deserialize(string filePath)
        {
            var lines = await FileHelper.ReadAllLines(filePath);

            var output = await this.BuildFromLines(lines);
            return output;
        }

        public async Task Serialize(string filePath, T value, bool overwrite = IOHelper.DefaultOverwriteValue)
        {
            var lines = await this.GetLines(value);

            await FileHelper.WriteAllLines(filePath, lines, overwrite);
        }
    }
}
