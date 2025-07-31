using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfExplorer
{
    public class FileHashCalculator
    {
        public enum HashAlgorithmType
        {
            MD5,
            SHA1,
            SHA256,
            SHA384,
            SHA512
        }

        public async Task<string> CalculateFileHashAsync(
            string filePath,
            HashAlgorithmType algorithmType = HashAlgorithmType.SHA256,
            int bufferSize = 4096 * 16) // Оптимальный размер буфера для HDD/SSD
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл не найден", filePath);

            HashAlgorithm hashAlgorithm = CreateHashAlgorithm(algorithmType);
            FileStream fileStream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize,
                FileOptions.SequentialScan | FileOptions.Asynchronous);

            var buffer = new byte[bufferSize];
            int bytesRead;

            while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                hashAlgorithm.TransformBlock(buffer, 0, bytesRead, null, 0);
            }

            hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
            return BitConverter.ToString(hashAlgorithm.Hash).Replace("-", "").ToLowerInvariant();
        }

        private HashAlgorithm CreateHashAlgorithm(HashAlgorithmType type)
        {
            HashAlgorithm value = null;

            switch (type)
            {
                case HashAlgorithmType.MD5: value = MD5.Create(); break;
                case HashAlgorithmType.SHA1: value = SHA1.Create(); break;
                case HashAlgorithmType.SHA256: value = SHA256.Create(); break;
                case HashAlgorithmType.SHA384: value = SHA384.Create(); break;
                case HashAlgorithmType.SHA512: value = SHA512.Create(); break;
                default:
                    throw new ArgumentException("Неподдерживаемый алгоритм хеширования");
            }            
            return value;
        }
    }

}
