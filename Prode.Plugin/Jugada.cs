using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prode.Plugin
{
    public class Jugada
    {

        IOrganizationService _crmService;
        public Jugada(IOrganizationService CRMService)
        {
            _crmService = CRMService;

        }

        /*755.230.001 acierto
         755.230.002 desacierto*/

        //statuscode predicción Op. Confirmada = 755.230.000
        public int calcularTotalPredicciones(Guid Jugadaid)
        {
            QueryExpression _qry;
               _qry = new QueryExpression("bar_jugadadetalle");//nombre entidad
                var filtro_identidad = new FilterExpression();
                filtro_identidad.AddCondition(new ConditionExpression("bar_jugada", ConditionOperator.Equal, Jugadaid));
                var fitro_statuscode = new FilterExpression();
                fitro_statuscode.AddCondition(new ConditionExpression("statuscode", ConditionOperator.Equal, 755230000)); // Predicción confirmada

                _qry.Criteria = new FilterExpression(LogicalOperator.And);
                _qry.Criteria.AddFilter(filtro_identidad);
                _qry.Criteria.AddFilter(fitro_statuscode);

                _qry.ColumnSet = new ColumnSet(false);
                EntityCollection qry_ec = _crmService.RetrieveMultiple(_qry);
            

            if (qry_ec.Entities.Count >= 0)
                    return qry_ec.Entities.Count;
                else
                    return 0;
        }
        public int calcularTotalAciertos(Guid Jugadaid)
        {
            QueryExpression _qry;
            _qry = new QueryExpression("bar_jugadadetalle");//nombre entidad
            var filtro_identidad = new FilterExpression();
            filtro_identidad.AddCondition(new ConditionExpression("bar_jugada", ConditionOperator.Equal, Jugadaid));
            var fitro_statuscode = new FilterExpression();
            fitro_statuscode.AddCondition(new ConditionExpression("statuscode", ConditionOperator.Equal, 755230000)); // Predicción confirmada
            var fitro_resultadoprediccion = new FilterExpression();
            fitro_resultadoprediccion.AddCondition(new ConditionExpression("bar_resultadoprediccion", ConditionOperator.Equal, 755230001)); // Acierto

            _qry.Criteria = new FilterExpression(LogicalOperator.And);
            _qry.Criteria.AddFilter(filtro_identidad);
            _qry.Criteria.AddFilter(fitro_statuscode);
            _qry.Criteria.AddFilter(fitro_resultadoprediccion);

            _qry.ColumnSet = new ColumnSet(false);
            EntityCollection qry_ec = _crmService.RetrieveMultiple(_qry);

            if (qry_ec.Entities.Count >= 0)
                return qry_ec.Entities.Count;
            else
                return 0;
        }
        public int calcularTotalDesaciertos(Guid Jugadaid)
        {
            QueryExpression _qry;
            _qry = new QueryExpression("bar_jugadadetalle");//nombre entidad
            var filtro_identidad = new FilterExpression();
            filtro_identidad.AddCondition(new ConditionExpression("bar_jugada", ConditionOperator.Equal, Jugadaid));
            var fitro_statuscode = new FilterExpression();
            fitro_statuscode.AddCondition(new ConditionExpression("statuscode", ConditionOperator.Equal, 755230000)); // Predicción confirmada
            var fitro_resultadoprediccion = new FilterExpression();
            fitro_resultadoprediccion.AddCondition(new ConditionExpression("bar_resultadoprediccion", ConditionOperator.Equal, 755230002)); // Desacierto

            _qry.Criteria = new FilterExpression(LogicalOperator.And);
            _qry.Criteria.AddFilter(filtro_identidad);
            _qry.Criteria.AddFilter(fitro_statuscode);
            _qry.Criteria.AddFilter(fitro_resultadoprediccion);

            _qry.ColumnSet = new ColumnSet(false);
            EntityCollection qry_ec = _crmService.RetrieveMultiple(_qry);

            if (qry_ec.Entities.Count >= 0)
                return qry_ec.Entities.Count;
            else
                return 0;
        }

        public bool validarJugadaExistente(Guid Jugadaid, Guid Torneoid, Guid Jugadorprodeid) {

            QueryExpression _qry;
            _qry = new QueryExpression("bar_jugada");//nombre entidad
            var filtro_identidad = new FilterExpression();
            filtro_identidad.AddCondition(new ConditionExpression("bar_jugada", ConditionOperator.Equal, Jugadaid));
            var fitro_statuscode = new FilterExpression();
            fitro_statuscode.AddCondition(new ConditionExpression("statuscode", ConditionOperator.Equal, 755230000)); // Predicción confirmada
            var fitro_resultadoprediccion = new FilterExpression();
            fitro_resultadoprediccion.AddCondition(new ConditionExpression("bar_resultadoprediccion", ConditionOperator.Equal, 755230002)); // Desacierto

            _qry.Criteria = new FilterExpression(LogicalOperator.And);
            _qry.Criteria.AddFilter(filtro_identidad);
            _qry.Criteria.AddFilter(fitro_statuscode);
            _qry.Criteria.AddFilter(fitro_resultadoprediccion);

            _qry.ColumnSet = new ColumnSet(false);
            EntityCollection qry_ec = _crmService.RetrieveMultiple(_qry);

            return false;
        }
    }
}
