using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prode.Plugin.Steps
{
    public class postUpdateJugada : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {

            ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);

            try {

                Entity Target = (Entity)context.InputParameters["Target"];

                if (Target.Contains("statuscode"))
                {
                    if (Target.GetAttributeValue<OptionSetValue>("statuscode").Value == 755230001)// Razon para el estado "Completada"
                        // Este plugin solo ejecuta cuando la jugada se marca como completada.
                    {
                        //Entity Jugada_entity = new Entity("bar_jugada");
                        Jugada Jugada = new Jugada(service);
                        Target.Attributes["bar_prediccionestotales"] = Jugada.calcularTotalPredicciones(Target.Id);
                        Target.Attributes["bar_aciertostotales"] = Jugada.calcularTotalAciertos(Target.Id);
                        Target.Attributes["bar_desaciertostotales"] = Jugada.calcularTotalDesaciertos(Target.Id);
                        Target.Attributes.Remove("statuscode");
                        service.Update(Target);
                    }
                }
            }
            catch (Exception e) {
                throw new InvalidPluginExecutionException("Error en la ejecución del plugin postUpdateJugada. Detalle del error: "+e.Message);
            }
        }
    }
}
