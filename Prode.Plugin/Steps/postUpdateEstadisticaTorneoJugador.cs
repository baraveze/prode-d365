
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace Prode.Plugin
{
    public class postUpdateEstadisticaTorneoJugador : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {

            ITracingService tracer = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            IOrganizationServiceFactory factory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            IOrganizationService service = factory.CreateOrganizationService(context.UserId);

            try
            {

                Entity Target = (Entity)context.InputParameters["Target"];
                Entity preImage = (Entity)context.PreEntityImages["preimage"];
                int puntajePorAcierto = 1;
                if (!Target.Contains("bar_fechadeproceso"))
                    throw new InvalidPluginExecutionException("El campo fecha proceso no fue actualizado. No se disparó correctamente el proceso.");

                if (!preImage.Contains("bar_jugadorprode"))
                    throw new InvalidPluginExecutionException("El valor del atributo Jugador Prode está vació o el valor fue nulo al momento de la ejecución.");

                if (!preImage.Contains("bar_torneo"))
                    throw new InvalidPluginExecutionException("El valor del atributo Torneo está vació o el valor fue nulo al momento de la ejecución.");

                if (Target.Contains("bar_puntajeporacierto"))
                    puntajePorAcierto = Target.GetAttributeValue<int>("bar_puntajeporacierto");
                else if(preImage.Contains("bar_puntajeporacierto"))
                        puntajePorAcierto = preImage.GetAttributeValue<int>("bar_puntajeporacierto");

                EstadisticaTorneoJugador obj = new EstadisticaTorneoJugador(service);
                Entity estadisticaTorneoJugador = new Entity("bar_estadisticatorneojugador");
                Guid Torneo = preImage.GetAttributeValue<EntityReference>("bar_torneo").Id;
                Guid JugadorProde = preImage.GetAttributeValue<EntityReference>("bar_jugadorprode").Id;
                estadisticaTorneoJugador.Id = Target.Id;
                estadisticaTorneoJugador.Attributes.Add("bar_puntajetotal", obj.calcularPuntajeTotal(Torneo, JugadorProde, puntajePorAcierto));
                estadisticaTorneoJugador.Attributes.Add("bar_puntajemaximoconseguido", obj.calcularAciertosMaximos(Torneo, JugadorProde));
                estadisticaTorneoJugador.Attributes.Add("bar_puntajeminimoconseguido", obj.calcularAciertosMinimos(Torneo, JugadorProde));
                estadisticaTorneoJugador.Attributes.Add("bar_cantidaddejugadas", obj.calcularCantidadDeJugadas(Torneo, JugadorProde));

                service.Update(estadisticaTorneoJugador);
                    
                
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException("Error en la ejecución del plugin postUpdateEstadisticaTorneoJugador. Detalle del error: " + ex.Message);
            }
        }
    }
}
