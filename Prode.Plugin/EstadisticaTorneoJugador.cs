using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prode.Plugin
{
    public class EstadisticaTorneoJugador
    {
        
        IOrganizationService _crmService;
        public EstadisticaTorneoJugador(IOrganizationService CRMService)
        {
            _crmService = CRMService;
          
        }

        public int calcularPuntajeTotal(Guid Torneo, Guid JugadorProde, int puntajePorAcierto)
        {
             int aciertostotales = 0;
            
                string bar_aciertostotales_sum = @"<fetch distinct='false' mapping='logical' aggregate='true'>
                   <entity name='bar_jugada'><attribute name='bar_aciertostotales' alias='bar_prediccionestotales_sum' aggregate='sum' />
                    <filter type='and'><condition attribute='bar_torneo' operator='eq' uitype='bar_torneo' value='{" + Torneo.ToString()
                       + "}' /> <condition attribute='bar_jugadorprode' operator='eq' uitype='bar_jugadorprode' value='{" + JugadorProde.ToString() + "}' />"
                       + "<condition attribute='statuscode' operator='eq' value='755230001' /></filter></entity></fetch>";
                EntityCollection bar_aciertostotales_sum_result = _crmService.RetrieveMultiple(new FetchExpression(bar_aciertostotales_sum));
                if (bar_aciertostotales_sum_result.Entities.Count > 0)
                {
                    foreach (var c in bar_aciertostotales_sum_result.Entities)
                    {
                        aciertostotales = ((int)((AliasedValue)c["bar_prediccionestotales_sum"]).Value);


                    }

                    return puntajePorAcierto * aciertostotales;
                }
                return 0;
            

           
        }
        public int calcularAciertosMaximos(Guid Torneo, Guid JugadorProde)
        {
                int aciertostotales = 0;
            if (Torneo != null && JugadorProde != null)
            {
                string bar_aciertostotales_max = @"<fetch distinct='false' mapping='logical' aggregate='true'>
                   <entity name='bar_jugada'><attribute name='bar_aciertostotales' alias='bar_prediccionestotales_max' aggregate='max' />
                    <filter type='and'><condition attribute='bar_torneo' operator='eq' uitype='bar_torneo' value='{" + Torneo.ToString()
                       + "}' /> <condition attribute='bar_jugadorprode' operator='eq' uitype='bar_jugadorprode' value='{" + JugadorProde.ToString() + "}' />"
                       + "<condition attribute='statuscode' operator='eq' value='755230001' /></filter></entity></fetch>";
                EntityCollection bar_aciertostotales_max_result = _crmService.RetrieveMultiple(new FetchExpression(bar_aciertostotales_max));
                if (bar_aciertostotales_max_result.Entities.Count > 0)
                {
                    foreach (var c in bar_aciertostotales_max_result.Entities)
                    {
                        aciertostotales = ((int)((AliasedValue)c["bar_prediccionestotales_max"]).Value);


                    }


                }
            }
            

            return aciertostotales;
        }

        public int calcularAciertosMinimos(Guid Torneo, Guid JugadorProde)
        {

            int aciertostotales = 0;
            if (Torneo != null && JugadorProde != null)
            {
                string bar_aciertostotales_min = @"<fetch distinct='false' mapping='logical' aggregate='true'>
                   <entity name='bar_jugada'><attribute name='bar_aciertostotales' alias='bar_prediccionestotales_min' aggregate='min' />
                    <filter type='and'><condition attribute='bar_torneo' operator='eq' uitype='bar_torneo' value='{" + Torneo.ToString()
                       + "}' /> <condition attribute='bar_jugadorprode' operator='eq' uitype='bar_jugadorprode' value='{" + JugadorProde.ToString() + "}' />"
                       + "<condition attribute='statuscode' operator='eq' value='755230001' /></filter></entity></fetch>";
                EntityCollection bar_aciertostotales_min_result = _crmService.RetrieveMultiple(new FetchExpression(bar_aciertostotales_min));
                if (bar_aciertostotales_min_result.Entities.Count > 0)
                {
                    foreach (var c in bar_aciertostotales_min_result.Entities)
                    {
                        aciertostotales = ((int)((AliasedValue)c["bar_prediccionestotales_min"]).Value);


                    }


                }
            }

            return aciertostotales;
        }

        public int calcularCantidadDeJugadas(Guid Torneo, Guid JugadorProde)
        {
            
           
            
                string cantidadDeJugadas = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                                    <entity name='bar_jugada'>
                                                    <attribute name='bar_jugadaid' />
                                                    <attribute name='bar_name' />
                                                    <attribute name='createdon' />
                                                    <order attribute='bar_name' descending='false' />
                                                    <filter type='and'>
                                                    <condition attribute='bar_torneo' operator='eq' uitype='bar_torneo' value='{"+Torneo.ToString()+"}' />" +
                                                    "<condition attribute='bar_jugadorprode' operator='eq' uitype='bar_jugadorprode' value='{"+JugadorProde.ToString()+"}' />" +
                                                    "<condition attribute='statuscode' operator='eq' value='755230001'/></filter>" +
                                                    "</entity>" +
                                                    "</fetch>"; //Filtra por torneo, jugador y estado de la jugada igual a Completada

        
                EntityCollection CantidadDeJugadas_Collection = _crmService.RetrieveMultiple(new FetchExpression(cantidadDeJugadas));
                if (CantidadDeJugadas_Collection.Entities.Count > 0)
                {
                return CantidadDeJugadas_Collection.Entities.Count;
                }
                return 0;
            }

         
        }
    }

