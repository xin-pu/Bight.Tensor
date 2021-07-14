using System.Linq;

namespace Bight.Tensor
{
    public partial class Tensor<T>
    {
        public override string ToString()
        {
            if (IsVector) return TensorTitle() + VectorToString(this) + "\r";
            if (IsMatrix) return TensorTitle() + MatrixToString(this) + "\r";
            if (IsTensor) return TensorTitle() + TensorToString(this) + "\r";
            return string.Empty;
        }

        internal string VectorToString(Tensor<T> vector)
        {
            var size = vector.Size[0];
            var data = Enumerable.Range(0, size)
                .Select(i => Holder.ToString(vector.GetValueNoCheck(i)));
            return $"[{string.Join("\t", data)}]";
        }

        internal string MatrixToString(Tensor<T> matrix)
        {
            var height = matrix.Size[0];
            var data = Enumerable.Range(0, height)
                .Select(i => VectorToString(matrix.GetSubTensor(i)));
            return $"[{string.Join(",\r", data)}]";
        }

        internal string TensorToString(Tensor<T> tensor)
        {
            if (tensor.Rank == 3)
            {
                var dims = tensor.Size[0];
                var data = Enumerable.Range(0, dims)
                    .Select(i => MatrixToString(tensor.GetSubTensor(i)));
                return $"[{string.Join("\r\r", data)}]";
            }
            else
            {
                var dims = tensor.Size[0];
                var data = Enumerable.Range(0, dims)
                    .Select(i => (i == 0 ? "" : " ") + TensorToString(tensor.GetSubTensor(i)));
                return $"({string.Join("\r\r", data)})";
            }
        }


        internal string TensorTitle()
        {
            var title = IsVector
                ? "Vector"
                : IsMatrix
                    ? "Matrix"
                    : "Tensor";
            return $"{title}({DType.Name})\t{Size}\r";
        }
    }
}