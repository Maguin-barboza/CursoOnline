using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CursoOnline.DominioTest._Utils
{
    public static class AssertException
    {
        public static void ComMensagem(this ArgumentException exception, string messagem)
        {
            if(exception.Message == messagem)
                Assert.True(true);
            else
                Assert.False(true, $"Mensagem esperada: '{exception.Message}'.\nMensagem que veio: '{messagem}'");
        }
    }
}
