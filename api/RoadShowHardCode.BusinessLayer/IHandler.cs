namespace RoadShowHardCode.BusinessLayer
{
    /// <summary>
    /// The Handler interface.
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <typeparam name="TParameters">
        /// Any parameters type
        /// </typeparam>
        /// <typeparam name="TResult">
        /// Any result type
        /// </typeparam>
        /// <returns>
        /// The <see cref="HandlerResult{T}"/> with <see cref="TResult"/> as parameter <c>T</c>.
        /// </returns>
        HandlerResult<TResult> Execute<TParameters, TResult>(TParameters parameters);
    }
}
